using System;
using System.Collections.Generic;

namespace DTS.Models
{
    public partial class TemplateState
    {
        public TemplateState()
        {
            Template = new HashSet<Template>();
            TemplateVersionControll = new HashSet<TemplateVersionControll>();
        }

        public int Id { get; set; }
        public string State { get; set; }

        public virtual ICollection<Template> Template { get; set; }
        public virtual ICollection<TemplateVersionControll> TemplateVersionControll { get; set; }
    }
}
