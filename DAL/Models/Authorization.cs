using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Pasword { get; set; }
        public UserStatus Status { get; set; }
        public User user { get; set; }
    }
}
