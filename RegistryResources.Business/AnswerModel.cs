using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class AnswerModel
    {
        [Key]
        public int AnswerId { get; set; }
        public int PatientId { get; set; }
        public string Answer { get; set; }
        public DateTime AnswerDate { get; set; }

        ICollection<QuestionAnswerModel> QuestionAnswers { get; } = new List<QuestionAnswerModel>();
    }
}
