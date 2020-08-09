using Microsoft.EntityFrameworkCore;
using RegistryResources.Business;
using System;

namespace RegistryResources.Data
{
    public class DataContextInMemory : DbContext, IDataContext
    {
        public DataContextInMemory(DbContextOptions<DataContextInMemory> options)
            : base(options)
        {

        }

        public DbSet<PatientModel> Patients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ProxyModel> Proxies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<RegistrantModel> Registrants { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<SurveyModel> Surveys { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<QuestionModel> Questions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<AnswerModel> Answers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<ResearcherModel> Researchers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<CultureModel> Cultures { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<AlertModel> Alerts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Save()
        {
            throw new NotImplementedException();
        }
    }
}
