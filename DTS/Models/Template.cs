using System;
using System.Collections.Generic;

namespace DTS.Models
{
    public partial class Template
    {
        public Template()
        {
            TemplateVersionControll = new HashSet<TemplateVersionControl>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int StateId { get; set; }

        public virtual User Owner { get; set; }
        public virtual TemplateState State { get; set; }
        public virtual ICollection<TemplateVersionControl> TemplateVersionControll { get; set; }
    }
}
