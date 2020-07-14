using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class CultureModel
    {
        [Key]
        public int CultureId { get; set; }
        public string Culture { get; set; }
        public string ItemType { get; set; }
        public string Keyword { get; set; }
        public string KeyText { get; set; }
    }
}
