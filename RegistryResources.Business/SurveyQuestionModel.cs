using System;
using System.Collections.Generic;
using System.Text;

namespace RegistryResources.Business
{
    public class SurveyQuestionModel
    {
        public int SurveyId { get; set; }
        public SurveyModel Survey { get; set; }
        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
    }
}
