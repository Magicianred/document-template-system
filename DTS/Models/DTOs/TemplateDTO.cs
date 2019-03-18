using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Models.DTOs
{
    public class TemplateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public List<TemplateVersionDTO> TemplateVersions { get; set; }

        public static TemplateDTO ParseTemplate(Template template)
        {
            var templateVersions = new List<TemplateVersionDTO>();
            foreach (var templateVersion in template.TemplateVersion)
            {
                templateVersions.Add(TemplateVersionDTO.ParseTemplateVersion(templateVersion));
            }

            return new TemplateDTO
            {
                Id = template.Id,
                Name = template.Name,
                OwnerId = template.OwnerId,
                TemplateVersions = templateVersions

            };
        }
    }

    

}
