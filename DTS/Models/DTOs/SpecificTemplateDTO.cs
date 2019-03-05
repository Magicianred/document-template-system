using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models.DTOs
{
    public class SpecificTemplateDTO
    {
        public int TemplateId { get; set; }
        public DateTime CreationTime { get; set; }
        public string TemplateVersion { get; set; }
        public string CreatorName { get; set; }
        public string CreatorMail { get; set; }
    }
}
