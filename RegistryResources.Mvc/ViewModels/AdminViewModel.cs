using RegistryResources.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryResources.Mvc.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<PatientModel> Patients { get; set; }
        public IEnumerable<ProxyModel> Proxies { get; set; }
        public IEnumerable<ResearcherModel> Researchers { get; set; }
    }
}
