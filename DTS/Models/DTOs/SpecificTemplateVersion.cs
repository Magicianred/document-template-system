using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models.DTOs
{
    public class SpecificTemplateVersion
    {
        public DateTime CreationTime { get; set; }
        public string TemplateVersion { get; set; }
        public UserDTO Creator { get; set; }
    }
}
