using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Models.DTOs
{
    public class UserPersonalData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public static UserPersonalData ParseUserPersonalData(User user)
        {
            return new UserPersonalData
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
        }
    }
}
