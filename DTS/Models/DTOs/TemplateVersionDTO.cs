using DAL.Models;
using System;
using System.Globalization;

namespace DTS.API.Models.DTOs
{
    public class TemplateVersionDTO
    {
        public int Id { get; set; }
        public string CreationTime { get; set; }
        public string Content { get; set; }
        public UserDTO Creator { get; set; }
        public string VersionState { get; set; }

        public static TemplateVersionDTO ParseTemplateVersion(TemplateVersion templateVersion)
        {
            return new TemplateVersionDTO
            {
                Id = templateVersion.Id,
                CreationTime = templateVersion.Date.ToString("dd.MM.yyyy HH:mm"),
                Content = templateVersion.Content,
                Creator = UserDTO.ParseUserDTO(templateVersion.Creator),
                VersionState = templateVersion.State.State
            };
        }
    }

}