using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RegistryResources.Business;

namespace RegistryResources.Data
{
    public interface IDataContext
    {
        DbSet<AlertModel> Alerts { get; set; }
        DbSet<PatientModel> Patients { get; set; }
        DbSet<ProxyModel> Proxies { get; set; }
        //DbSet<RegistrantModel> Registrants { get; set; }
        DbSet<SurveyModel> Surveys { get; set; }
        DbSet<QuestionModel> Questions { get; set; }
        DbSet<AnswerModel> Answers { get; set; }
        DbSet<CultureModel> Cultures { get; set; }
        DbSet<ResearcherModel> Researchers { get; set; }

        int Save();
    }
}
