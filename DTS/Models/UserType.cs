using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models
{
    public class UserType
    {
        public int ID { get; set; }
        public string Type { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
