using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models.DTOs
{
    public class ExtendedUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public UserType Type { get; set; }
    }
}
