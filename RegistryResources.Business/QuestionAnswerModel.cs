using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistryResources.Business
{
    public class QuestionAnswerModel
    {
        [Key]
        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
        public int AnswerId { get; set; }
        public AnswerModel Answer { get; set; }
        public int SurveyId { get; set; }
        public int PatientId { get; set; }
    }
}
