using System;
using System.Collections.Generic;

namespace DTS.Models
{
    public partial class Status
    {
        public Status()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
