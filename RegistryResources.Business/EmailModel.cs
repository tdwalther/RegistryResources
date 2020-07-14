using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class EmailModel
    {
        [Key]
        public int EmailId { get; set; }
        public string EmailAddress { get; set; }
        public bool BadEmail { get; set; }
        public bool NoEmail { get; set; }
        public bool NeedsVerification { get; set; }

    }
}
