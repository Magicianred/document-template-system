using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Models.DTOs
{
    public class SpecificTemplateVersion
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string TemplateVersion { get; set; }
        public UserDTO Creator { get; set; }
        public string VersionState { get; set; }
    }
}
