using System;
using System.Collections.Generic;

namespace DTS.Models
{
    public partial class Template
    {
        public Template()
        {
            TemplateVersionControll = new HashSet<TemplateVersionControll>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<TemplateVersionControll> TemplateVersionControll { get; set; }
    }
}
