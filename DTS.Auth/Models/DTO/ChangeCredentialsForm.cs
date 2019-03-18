using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Auth.Models.DTO
{
    public class ChangeCredentialsForm
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewLogin { get; set; }
        public string NewPassword { get; set; }
    }
}
