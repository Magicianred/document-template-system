using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class User
    {
        public User()
        {
            Template = new HashSet<Template>();
            TemplateVersion = new HashSet<TemplateVersion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        public virtual UserStatus Status { get; set; }
        public virtual UserType Type { get; set; }
        public virtual ICollection<Template> Template { get; set; }
        public virtual ICollection<TemplateVersion> TemplateVersion { get; set; }
    }
}
