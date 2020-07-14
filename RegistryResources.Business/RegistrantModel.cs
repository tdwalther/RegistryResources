using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class RegistrantModel
    {
        [Key]
        public int RegistrantId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public AddressModel Address { get; set; }
        public EmailModel Email { get; set; }
        public PhoneModel Phone { get; set; }

        public RegistrantModel()
        {
            Address = new AddressModel();
            Email = new EmailModel();
            Phone = new PhoneModel();
        }
    }
}
