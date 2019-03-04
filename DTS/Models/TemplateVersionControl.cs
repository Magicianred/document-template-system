using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models
{
    public class TemplateVersionControl
    {
        public int ID { get; set; }
        public DateTime CreationData {get; set;}
        public string TemplateVersion { get; set; }
        public int TemplateID { get; set; }
        public int UserID { get; set; }

        
        public TemplateState TemplateState { get; set; }
        [NotMapped]
        public User User { get; set; }
    }
}
