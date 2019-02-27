using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models
{
    public class UserStatus
    {
        public int ID { get; set; }
        public string Status { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
