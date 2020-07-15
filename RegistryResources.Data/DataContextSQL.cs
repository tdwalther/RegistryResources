using Microsoft.EntityFrameworkCore;
using RegistryResources.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistryResources.Data
{
    public class DataContextSQL : DbContext, IDataContext
    {
        private string _ConnectionString = null;

        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<ProxyModel> Proxies { get; set; }
        public DbSet<ResearcherModel> Researchers { get; set; }
        public DbSet<RegistrantModel> Registrants { get; set; }
        public DbSet<SurveyModel> Surveys { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<CultureModel> Cultures { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("OnConfiguring");
            optionsBuilder.UseSqlServer(_ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
            modelBuilder.Entity<RegistrantModel>().ToTable("Registrants");
            modelBuilder.Entity<PhoneModel>().ToTable("Phones");
            modelBuilder.Entity<EmailModel>().ToTable("Emails");
            modelBuilder.Entity<AddressModel>().ToTable("Addresses");
            modelBuilder.Entity<SurveyModel>().ToTable("Surveys");
            modelBuilder.Entity<QuestionModel>().ToTable("Questions");
            modelBuilder.Entity<PatientProxyModel>().ToTable("PatientProxies");
            modelBuilder.Entity<SurveyQuestionModel>().ToTable("SurveyQuestions");
            modelBuilder.Entity<CultureModel>().ToTable("Cultures");
            
            modelBuilder.Entity<AnswerModel>().ToTable("Answers");
            modelBuilder.Entity<QuestionAnswerModel>().ToTable("QuestionAnswers");

            modelBuilder.Entity<PatientModel>()
                .HasOne(p => p.Registrant);

            modelBuilder.Entity<ProxyModel>()
                .HasOne(p => p.Registrant);

            modelBuilder.Entity<ResearcherModel>()
                .HasOne(r => r.Registrant);

            modelBuilder.Entity<RegistrantModel>()
                .HasOne(p => p.Address);

            modelBuilder.Entity<RegistrantModel>()
                .HasOne(p => p.Email);

            modelBuilder.Entity<RegistrantModel>()
                .HasOne(p => p.Phone);

            modelBuilder.Entity<PatientProxyModel>()
                .HasKey(bc => new { bc.PatientId, bc.ProxyId });

            modelBuilder.Entity<SurveyQuestionModel>()
                .HasKey(bc => new { bc.SurveyId, bc.QuestionId });

            modelBuilder.Entity<QuestionAnswerModel>()
                .HasKey(bc => new { bc.QuestionId, bc.AnswerId });
        }

        public DataContextSQL()
        {
            _ConnectionString = "Server = (localdb)\\mssqllocaldb; Database = RegistryResources; Trusted_Connection = True; MultipleActiveResultSets = true";
        }

        public DataContextSQL(string connectionString=null)
        {
            _ConnectionString = connectionString;
        }
    }
}
