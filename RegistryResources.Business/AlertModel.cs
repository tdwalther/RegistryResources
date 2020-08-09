using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistryResources.Business
{
    public class AlertModel
    {
        [Key]
        public int AlertId { get; set; }
        public int RecipientId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DisplayDate { get; set; }
        public int ReplayCount { get; set; }
        public string Message { get; set; }
        public bool Display { get; set; }
    }
}
