using System;
using System.Collections.Generic;

namespace DTS.Models
{
    public partial class User
    {
        public User()
        {
            TemplateVersionControll = new HashSet<TemplateVersionControll>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        public virtual Status Status { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<TemplateVersionControll> TemplateVersionControll { get; set; }
    }
}
