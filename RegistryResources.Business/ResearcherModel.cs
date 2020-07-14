using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class ResearcherModel
    {
        [Key]
        public int ResearcherId { get; set; }
        public RegistrantModel Registrant { get; set; }
        public string InstitutionName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}
