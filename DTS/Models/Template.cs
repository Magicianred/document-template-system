using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models
{
    public class Template
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TemplateStateID { get; set; }
    
        public TemplateState TemplateState { get; set; }

    }
}
