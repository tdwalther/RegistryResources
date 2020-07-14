using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class ProxyModel
    {
        [Key]
        public int ProxyId { get; set; }
        public RegistrantModel Registrant { get; set; }
        public DateTime BirthDate { get; set; }
        public List<PatientProxyModel> PatientProxy { get; } = new List<PatientProxyModel>();
    }
}
