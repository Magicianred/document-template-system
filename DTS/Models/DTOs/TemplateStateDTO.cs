using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Models.DTOs
{
    public class TemplateStateDTO
    {
        public string Name { get; set; }

        public static TemplateStateDTO ParseTemplateStateDTO(TemplateState templateState)
        {
            return new TemplateStateDTO
            {
                Name = templateState.State,
            };
        }
    }

}
