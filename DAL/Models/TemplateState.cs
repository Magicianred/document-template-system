using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class TemplateState
    {
        public TemplateState()
        {
            Template = new HashSet<Template>();
            TemplateVersion = new HashSet<TemplateVersion>();
        }

        public int Id { get; set; }
        public string State { get; set; }

        public virtual ICollection<Template> Template { get; set; }
        public virtual ICollection<TemplateVersion> TemplateVersion { get; set; }
    }
}
