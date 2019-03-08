using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Template
    {
        public Template()
        {
            TemplateVersion = new HashSet<TemplateVersion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int StateId { get; set; }

        public virtual User Owner { get; set; }
        public virtual TemplateState State { get; set; }
        public virtual ICollection<TemplateVersion> TemplateVersion { get; set; }
    }
}
