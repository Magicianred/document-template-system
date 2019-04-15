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

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TemplateState ts = (TemplateState)obj;
                return (Id == ts.Id) && (State == ts.State);
            }
        }

        public override int GetHashCode() => HashCode.Combine(Id, State);

    }
}
