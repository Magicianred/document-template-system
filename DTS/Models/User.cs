﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models
{
    public class User
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        public int UserStatusID { get; set; }

        public UserStatus Status { get; set; }
        public UserType Type { get; set; }
        public virtual ICollection<TemplateVersionControl> Templates { get; set; }
    }
}
