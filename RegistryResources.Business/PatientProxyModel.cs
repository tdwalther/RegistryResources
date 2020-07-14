using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class PatientProxyModel
    {
        [Key]
        public int PatientId { get; set; }
        public PatientModel Patient { get; set; }
        public int ProxyId { get; set; }
        public ProxyModel Proxy { get; set; }
        public string Relationship { get; set; }
    }
}
