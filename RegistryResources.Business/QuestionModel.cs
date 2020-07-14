using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistryResources.Business
{
    public enum QuestionTypes
    {
        SingleSelect,
        MultiSelect,
        FreeText,
        DateTime
    }

    public class QuestionModel
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionKey { get; set; }
        [NotMapped]
        public Dictionary<string, string> dicQuestion { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public string ConstraintKey { get; set; }
        [NotMapped]
        public string ConstrainedValues { get; set; }
        [NotMapped]
        public Dictionary<string, Dictionary<string, string>> dicConstrainedValues { get; set; }
        public bool IsRequired { get; set; }
        public ICollection<QuestionAnswerModel> QuestionAnswers { get; } = new List<QuestionAnswerModel>();
        public ICollection<SurveyQuestionModel> SurveyQuestions { get; } = new List<SurveyQuestionModel>();

        public QuestionModel()
        {
            dicQuestion = new Dictionary<string, string>();
        }
    }
}
