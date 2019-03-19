using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Models.DTOs
{
    public class ExtendedUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public static ExtendedUserDTO ParseUserDTO(User user)
        {
            return new ExtendedUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Status = user.Status.Name,
                Type = user.Type.Name
            };
        }


        public static List<ExtendedUserDTO> ParseUsersDTO(IEnumerable<User> users)
        {
            var usersDTO = new List<ExtendedUserDTO>();

            foreach (var user in users)
            {
                usersDTO.Add(ParseUserDTO(user));
            }

            return usersDTO;
        }
    }
}
