using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Models.DTOs
{
    public class UserPersonalDataDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public static UserPersonalDataDTO ParseUserPersonalDataDTO(User user)
        {
            return new UserPersonalDataDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
        }
    }
}
