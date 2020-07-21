using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryResources.Mvc.Models
{
    public class MetricsViewModel
    {
        public List<Tuple<string,int>> Counters { get; set; }

        public MetricsViewModel()
        {
            Counters = new List<Tuple<string, int>>();
        }
    }
}
