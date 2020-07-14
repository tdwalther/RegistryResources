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
        public string Title { get; set; }
        public string Description { get; set; }
        public string IRBNumber { get; set; }
        public string SurveyState { get; set; } //new, pending, recruiting, running?, closed
        public DateTime SurveyDate { get; set; }
        public ICollection<SurveyQuestionModel> SurveyQuestions { get; } = new List<SurveyQuestionModel>();
    }
}
