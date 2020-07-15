using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class SurveyModel
    {
        [Key]
        public int SurveyId { get; set; }
        public ResearcherModel Researcher { get; set; }
        public string TitleKey { get; set; }
        public string DescriptionKey { get; set; }
        public string IRBNumber { get; set; }
        public string SurveyStateKey { get; set; } //new, pending, recruiting, running?, closed
        public DateTime SurveyDate { get; set; }
        public ICollection<SurveyQuestionModel> SurveyQuestions { get; } = new List<SurveyQuestionModel>();
    }
}
