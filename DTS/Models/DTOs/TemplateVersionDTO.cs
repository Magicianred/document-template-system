using DAL.Models;
using System;

namespace DTS.API.Models.DTOs
{
    public class TemplateVersionDTO
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Content { get; set; }
        public UserDTO Creator { get; set; }
        public string VersionState { get; set; }

        public static TemplateVersionDTO ParseTemplateVersion(TemplateVersion templateVersion)
        {
            return new TemplateVersionDTO
            {
                Id = templateVersion.Id,
                CreationTime = templateVersion.Date,
                Content = templateVersion.Content,
                Creator = UserDTO.ParseUserDTO(templateVersion.Creator),
                VersionState = templateVersion.State.State
            };
        }
    }

}