using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models.DTOs
{
    public class SpecificTemplate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<SpecificTemplateVersion> Versions { get; set; }
    }
}
