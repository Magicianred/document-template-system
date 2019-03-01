using System;
using System.Collections.Generic;
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

        public Template Template { get; set; }
        public TemplateState TemplateState { get; set; }
        public User User { get; set; }
    }
}
