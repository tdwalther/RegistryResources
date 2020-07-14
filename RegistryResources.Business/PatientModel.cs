using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class PatientModel
    {
        [Key]
        public int PatientId { get; set; }
        public RegistrantModel Registrant { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<PatientProxyModel> PatientProxy { get; } = new List<PatientProxyModel>();
    }
}
