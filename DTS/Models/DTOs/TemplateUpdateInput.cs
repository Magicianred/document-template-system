using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Models
{
    public class TemplateUpdateInput
    {
        public string Name { get; set; }
        public int StateId { get; set; }
        public int OwnerID { get; set; }
    }
}
