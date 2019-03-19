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
        public UserDTO Owner { get; set; }
        public List<TemplateVersionDTO> TemplateVersions { get; set; }


        public static TemplateDTO ParseTemplateDTO(Template template)
        {
            var userDTO = UserDTO.ParseUserDTO(template.Owner);

            var templateVersions = new List<TemplateVersionDTO>();
            foreach (var templateVersion in template.TemplateVersion)
            {
                templateVersions.Add(TemplateVersionDTO.ParseTemplateVersion(templateVersion));
            }

            return new TemplateDTO
            {
                Id = template.Id,
                Name = template.Name,
                Owner = userDTO,
                TemplateVersions = templateVersions
            };
        }


        public static List<TemplateDTO> ParseTemplatesDTO(IEnumerable<Template> templates)
        {
            var templatesDTO = new List<TemplateDTO>();

            foreach (var template in templates)
            {
                templatesDTO.Add(ParseTemplateDTO(template));
            }

            return templatesDTO;
        }
    }
}

