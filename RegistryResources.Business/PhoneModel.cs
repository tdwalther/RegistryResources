using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class PhoneModel
    {
        [Key]
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
        public bool BadPhone { get; set; }
        public bool NoPhone { get; set; }
        public bool NeedsVerification { get; set; }
    }
}
