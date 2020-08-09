using LocationStandard;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RandomStandard;
using RegistryResources.Business;
using RegistryResources.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegistryResources.TestConsole
{
    class Program
    {
        static string _ConnectionString = "Server = (localdb)\\mssqllocaldb; Database = RegistryResources; Trusted_Connection = True; MultipleActiveResultSets = true";

        static void Main(string[] args)
        {
            CreateAlerts();
            //CreatePatientsAndProxies(500);
            //CreateResearchers(10);
            //CreateSurveys();
            //DoSurveyTranslations("en-US");
            //DoSurveyTranslations("es-MX");
            //DoSurveyTranslations("ru-RU");
            //DoQuestionTranslations("en-US");
            //DoQuestionTranslations("es-MX");
            //DoQuestionTranslations("ru-RU");
        }

        #region methods used to create alerts

        static void CreateAlerts()
        {
            using (DataContextSQL context = new DataContextSQL(_ConnectionString))
            {
                var alerts = new List<AlertModel>();

                var registrants = context.Patients.Select(pat => new
                {
                    regId = pat.Registrant.RegistrantId,
                    userId = pat.Registrant.UserId
                }).ToList();

                registrants.AddRange(context.Proxies.Select(pxy => new 
                { 
                    regId = pxy.Registrant.RegistrantId,
                    userId = pxy.Registrant.UserId
                }));

                registrants.AddRange(context.Researchers.Select(res => new
                {
                    regId = res.Registrant.RegistrantId,
                    userId = res.Registrant.UserId
                }));

                foreach (var reg in registrants)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (RandomNumbers.GetDouble() > 0.5)
                        {
                            alerts.Add(new AlertModel()
                            {
                                CreateDate = DateTime.Now,
                                Display = true,
                                DisplayDate = DateTime.Now,
                                Message = LoremNET.Lorem.Sentence(3, 12),
                                RecipientId = reg.regId,
                                UserId = reg.userId,
                                ReplayCount = 4
                            });
                        }
                    }
                }
                context.Alerts.AddRange(alerts);
                int retval  = context.Save();
            }
        }

        #endregion

        #region methods used to create patients and proxies

        static void CreatePatientsAndProxies(int numPatients)
        {
            using (DataContextSQL context = new DataContextSQL(_ConnectionString))
            {
                for (int i = 0; i < numPatients; i++)
                {
                    List<PatientProxyModel> pps = AddPatientProxySet();
                    if (pps.First().Proxy == null)
                    {
                        context.Patients.Add(pps.First().Patient);
                    }
                    else
                    {
                        context.AddRange(pps);
                    }
                }
                context.SaveChanges();
            }
        }

        static List<PatientProxyModel> AddPatientProxySet()
        {
            var rnd = RandomNumbers.GetDouble();
            List<PatientProxyModel> patientProxies = new List<PatientProxyModel>();

            NameModel name1 = (RandomNumbers.GetDouble() > 0.5) ? RandomStrings.GetName("M") : RandomStrings.GetName("F");
            NameModel name2 = (RandomNumbers.GetDouble() > 0.5) ? RandomStrings.GetName("M") : RandomStrings.GetName("F");
            NameModel name3 = RandomStrings.GetName("F");
            NameModel name4 = RandomStrings.GetName("M");

            name2.LName = name1.LName;
            name3.LName = name1.LName;
            name4.LName = name1.LName;

            DateTime birthdate1 = new DateTime(
                DateTime.Today.Year - 14 - RandomNumbers.GetInteger(40),
                RandomNumbers.GetInteger(12) + 1,
                RandomNumbers.GetInteger(28) + 1);

            DateTime birthdate2 = new DateTime(
                birthdate1.Year - RandomNumbers.GetInteger(8),
                RandomNumbers.GetInteger(12) + 1,
                RandomNumbers.GetInteger(28) + 1);

            DateTime birthdate3 = new DateTime(
                birthdate2.Year - 20 - RandomNumbers.GetInteger(12),
                RandomNumbers.GetInteger(12) + 1,
                RandomNumbers.GetInteger(28) + 1);

            DateTime birthdate4 = new DateTime(
                birthdate2.Year - 20 - RandomNumbers.GetInteger(12),
                RandomNumbers.GetInteger(12) + 1,
                RandomNumbers.GetInteger(28) + 1);

            AddressModel address1 = GetAddress();
            AddressModel address2 = GetAddress();

            PatientModel pat1 = new PatientModel()
            {
                Registrant = new RegistrantModel()
                {
                    Address = address1.Clone() as AddressModel,
                    Phone = GetPhone(),
                    Email = GetEmail(name1.FName, name1.LName),
                    FirstName = name1.FName,
                    LastName = name1.LName,
                    MiddleName = name1.MName
                },
                BirthDate = birthdate1
            };

            PatientModel pat2 = new PatientModel()
            {
                Registrant = new RegistrantModel()
                {
                    Address = address1.Clone() as AddressModel,
                    Phone = GetPhone(),
                    Email = GetEmail(name2.FName, name2.LName),
                    FirstName = name2.FName,
                    LastName = name2.LName,
                    MiddleName = name2.MName
                },
                BirthDate = birthdate2
            };

            ProxyModel pxy1 = new ProxyModel()
            {
                Registrant = new RegistrantModel()
                {
                    Address = address1.Clone() as AddressModel,
                    Phone = GetPhone(),
                    Email = GetEmail(name3.FName, name3.LName),
                    FirstName = name3.FName,
                    LastName = name3.LName,
                    MiddleName = name3.MName
                },
                BirthDate = birthdate3
            };

            ProxyModel pxy2 = new ProxyModel()
            {
                Registrant = new RegistrantModel()
                {
                    Address = RandomNumbers.GetDouble() > 0.75 ? address1.Clone() as AddressModel : address2.Clone() as AddressModel,
                    Phone = GetPhone(),
                    Email = GetEmail(name4.FName, name4.LName),
                    FirstName = name4.FName,
                    LastName = name4.LName,
                    MiddleName = name4.MName
                },
                BirthDate = birthdate4,
            };

            if (rnd < 0.1)
            {
                //multiple patients sharing proxies

                patientProxies.Add(new PatientProxyModel()
                {
                    Patient = pat1,
                    Proxy = pxy1,
                    Relationship = "Parent"
                });

                patientProxies.Add(new PatientProxyModel()
                {
                    Patient = pat1,
                    Proxy = pxy2,
                    Relationship = "Parent"
                });

                patientProxies.Add(new PatientProxyModel()
                {
                    Patient = pat2,
                    Proxy = pxy1,
                    Relationship = "Parent"
                });

                patientProxies.Add(new PatientProxyModel()
                {
                    Patient = pat2,
                    Proxy = pxy2,
                    Relationship = "Parent"
                });

            }
            else if (rnd > 0.8)
            {
                //one patient with proxies

                patientProxies.Add(new PatientProxyModel()
                {
                    Patient = pat1,
                    Proxy = pxy1
                });

                if (RandomNumbers.GetDouble() > 0.25)
                {
                    patientProxies.Add(new PatientProxyModel()
                    {
                        Patient = pat1,
                        Proxy = pxy2
                    });
                }
            }
            else
            {
                // one patient with no proxies

                patientProxies.Add(new PatientProxyModel()
                {
                    Patient = pat1,
                    Proxy = null
                });
            }

            return patientProxies;
        }

        private static int GetAge(DateTime date)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            DateTime today = DateTime.Today;

            TimeSpan span = today - date;
            // Because we start at year 1 for the Gregorian
            // calendar, we must subtract a year here.
            int years = (zeroTime + span).Year - 1;
            return years;
        }

        private static AddressModel GetAddress()
        {
            LocationModel loc = LocationFactory2.GetLocation();
            string street = $"{RandomNumbers.GetInteger(999)} {RandomStrings.GetStreetName()}";
            string line2 = RandomNumbers.GetDouble() > 0.75 ? $"Apt. {RandomNumbers.GetInteger(100)}" : "";
            string zip = $"{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}";

            AddressModel address = new AddressModel()
            {
                Street1 = street,
                Street2 = line2,
                City = loc.City,
                State = loc.State,
                PostalCode = zip,
                Latitide = loc.Lat,
                Longitude = loc.Lon,
                BadAddress = RandomNumbers.GetDouble() > 0.95,
                NeedsVerification = RandomNumbers.GetDouble() > 0.95,
                NoMail = RandomNumbers.GetDouble() > 0.95
            };

            return address;
        }

        private static PhoneModel GetPhone()
        {
            string[] phonetypes = { "mobile", "home" };

            PhoneModel phone = new PhoneModel()
            {
                PhoneNumber = $"({RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}) {RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}-{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}{RandomNumbers.GetInteger(10)}",
                PhoneType = phonetypes[RandomNumbers.GetInteger(phonetypes.Count())],
                BadPhone = RandomNumbers.GetDouble() > 0.95,
                NeedsVerification = RandomNumbers.GetDouble() > 0.95,
                NoPhone = RandomNumbers.GetDouble() > 0.95
            };
            return phone;
        }

        private static EmailModel GetEmail(
            string firstName,
            string lastName)
        {
            string[] emailcarriers = { "gmail.com", "verison.net", "hotmail.com", "yahoo.com", "comcast.net" };
            string emailaddress = "";
            double rnd = RandomNumbers.GetDouble();

            if (rnd <= 0.33)
            {
                emailaddress = $"{firstName.ToLower()}.{lastName.ToLower()}@{emailcarriers[RandomNumbers.GetInteger(emailcarriers.Count())]}";
            }
            else if (rnd > 0.66)
            {
                emailaddress = $"{firstName.ToLower()}{lastName.ToLower()}@{emailcarriers[RandomNumbers.GetInteger(emailcarriers.Count())]}";
            }
            else
            {
                emailaddress = $"{firstName.ToLower()[0]}{lastName.ToLower()}@{emailcarriers[RandomNumbers.GetInteger(emailcarriers.Count())]}";
            }

            return new EmailModel()
            {
                EmailAddress = emailaddress,
                BadEmail = RandomNumbers.GetDouble() > 0.95,
                NoEmail = RandomNumbers.GetDouble() > 0.95,
                NeedsVerification = RandomNumbers.GetDouble() > 0.95
            };


        }

        #endregion


        #region methods used to create reseaechers

        static void CreateResearchers(int numResearchers)
        {
            using (DataContextSQL context = new DataContextSQL(_ConnectionString))
            {
                for (int i = 0; i < numResearchers; i++)
                {
                    NameModel name1 = (RandomNumbers.GetDouble() > 0.5) ? RandomStrings.GetName("M") : RandomStrings.GetName("F");
                    NameModel name2 = (RandomNumbers.GetDouble() > 0.5) ? RandomStrings.GetName("M") : RandomStrings.GetName("F");

                    RegistrantModel reg = new RegistrantModel()
                    {
                        Address = GetAddress(),
                        Phone = GetPhone(),
                        Email = GetEmail(name1.FName, name1.LName),
                        LastName = name1.LName,
                        FirstName = name1.FName,
                        MiddleName = name1.MName
                    };

                    ResearcherModel res = new ResearcherModel()
                    {
                        Registrant = reg,
                        ContactEmail = GetEmail(name2.FName, name2.LName).EmailAddress,
                        ContactName = $"{name2.FName} {name2.LName}",
                        ContactPhone = GetPhone().PhoneNumber,
                        InstitutionName = RandomStrings.GetTownName()
                    };
                    context.Researchers.Add(res);
                }

                context.SaveChanges();

            }
        }

        #endregion


        #region methods used to create surveys

        static void CreateSurveys()
        {
            using (DataContextSQL context = new DataContextSQL(_ConnectionString))
            {
                var researchers = context.Researchers.ToList();
                context.AddRange(AddSurveyQuestions(researchers));
                context.SaveChanges();
            }
        }

        static List<SurveyQuestionModel> AddSurveyQuestions(
            List<ResearcherModel> researchers)
        {
            List<SurveyModel> Surveys = new List<SurveyModel>();
            List<QuestionModel> Questions = new List<QuestionModel>();
            List<SurveyQuestionModel> SurveyQuestions = new List<SurveyQuestionModel>();

            Surveys.Add(new SurveyModel()
            {
                TitleKey = "Diagnosis Title",
                DescriptionKey = "Diagnosis Description",
                IRBNumber = "IRB112022",
                SurveyDate = DateTime.Today,
                SurveyStateKey = "New",
                Researcher = researchers[RandomNumbers.GetInteger(researchers.Count)]
            });

            Questions.Add(new QuestionModel()
            {
                QuestionKey = "Confirmed Diagnosis",
                QuestionType = QuestionTypes.SingleSelect,
                IsRequired = true,
                ConstraintKey = "Yes/No",
            });

            SurveyQuestions.Add(new SurveyQuestionModel()
            {
                Survey = Surveys.Last(),
                Question = Questions.Last()
            });

            Questions.Add(new QuestionModel()
            {
                QuestionKey = "Rare Diagnosis",
                QuestionType = QuestionTypes.SingleSelect,
                IsRequired = true,
                ConstraintKey = "Yes/No",
            });

            SurveyQuestions.Add(new SurveyQuestionModel()
            {
                Survey = Surveys.Last(),
                Question = Questions.Last()
            });

            Questions.Add(new QuestionModel()
            {
                QuestionKey = "Which Disease",
                QuestionType = QuestionTypes.FreeText,
                IsRequired = false,
                ConstraintKey = "Yes/No",
            });

            SurveyQuestions.Add(new SurveyQuestionModel()
            {
                Survey = Surveys.Last(),
                Question = Questions.Last()
            });

            Surveys.Add(new SurveyModel()
            {
                TitleKey = "Medical History Title",
                DescriptionKey = "Medical History Description",
                IRBNumber = "IRB345763",
                SurveyDate = DateTime.Today,
                SurveyStateKey = "New",
                Researcher = researchers[RandomNumbers.GetInteger(researchers.Count)]
            });

            Questions.Add(new QuestionModel()
            {
                QuestionKey = "Current Health Concerns",
                QuestionType = QuestionTypes.MultiSelect,
                IsRequired = true,
                ConstraintKey = "balance issues/coordination issues/vision issues",
            });

            SurveyQuestions.Add(new SurveyQuestionModel()
            {
                Survey = Surveys.Last(),
                Question = Questions.Last()
            });

            Questions.Add(new QuestionModel()
            {
                QuestionKey = "Diagnosed With Diabetes",
                QuestionType = QuestionTypes.SingleSelect,
                IsRequired = true,
                ConstraintKey = "Yes/No",
            });

            SurveyQuestions.Add(new SurveyQuestionModel()
            {
                Survey = Surveys.Last(),
                Question = Questions.Last()
            });

            Questions.Add(new QuestionModel()
            {
                QuestionKey = "Diagnosed With Heart Condition",
                QuestionType = QuestionTypes.MultiSelect,
                IsRequired = true,
                ConstraintKey = "cardiomyopathy/arrythmia/heart failure/hypertophy",
            });

            SurveyQuestions.Add(new SurveyQuestionModel()
            {
                Survey = Surveys.Last(),
                Question = Questions.Last()
            });

            Questions.Add(new QuestionModel()
            {
                QuestionKey = "Diagnosed With Scoliosis",
                QuestionType = QuestionTypes.SingleSelect,
                IsRequired = true,
                ConstraintKey = "Yes/No",
            });

            SurveyQuestions.Add(new SurveyQuestionModel()
            {
                Survey = Surveys.Last(),
                Question = Questions.Last()
            });

            return SurveyQuestions;
        }

        #endregion


        #region methods user to answer surveys



        #endregion


        #region methods used to populate translations

        static void DoQuestionTranslations(string culture)
        {
            using (DataContextSQL context = new DataContextSQL(_ConnectionString))
            {
                var questions = context.Questions.Select(q => q.QuestionKey).Distinct().ToList();
                var rawConstraints = context.Questions.Select(q => q.ConstraintKey).ToList();
                var constraints = new List<string>();

                foreach (var con in rawConstraints)
                {
                    constraints.AddRange(con.Split("/".ToCharArray()));
                }
                constraints = constraints.Distinct().ToList();

                foreach (var con in constraints)
                {
                    context.Cultures.Add(new CultureModel()
                    {
                        Culture = culture,
                        ItemType = "constraint",
                        Keyword = con
                    });
                }

                foreach (var ques in questions)
                {
                    context.Cultures.Add(new CultureModel()
                    {
                        Culture = culture,
                        ItemType = "question",
                        Keyword = ques
                    });
                }

                context.SaveChanges();
            }
        }

        static void DoSurveyTranslations(string culture)
        {
            using (DataContextSQL context = new DataContextSQL(_ConnectionString))
            {
                var surveys = context.Surveys.Distinct().ToList();
                var titles = surveys.Select(s => s.TitleKey).Distinct().ToList();
                var descriptions = surveys.Select(s => s.DescriptionKey).Distinct().ToList();
                var states = surveys.Select(s => s.SurveyStateKey).Distinct().ToList();

                foreach (var title in titles)
                {
                    context.Cultures.Add(new CultureModel()
                    {
                        Culture = culture,
                        ItemType = "survey",
                        Keyword = title
                    });
                }

                foreach (var desc in descriptions)
                {
                    context.Cultures.Add(new CultureModel()
                    {
                        Culture = culture,
                        ItemType = "survey",
                        Keyword = desc
                    });
                }

                foreach (var state in states)
                {
                    context.Cultures.Add(new CultureModel()
                    {
                        Culture = culture,
                        ItemType = "survey",
                        Keyword = state
                    });
                }

                context.SaveChanges();
            }
        }

        #endregion

    }
}
