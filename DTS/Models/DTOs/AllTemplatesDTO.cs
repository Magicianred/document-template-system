using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models
{
    public class AllTemplatesDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TemplateState { get; set; }
        public int VersionCount { get; set; }
        public string OwnerName { get; set; }
        public string OwnerMail { get; set; }
    }
}
