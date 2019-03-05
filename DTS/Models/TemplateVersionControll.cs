using System;
using System.Collections.Generic;

namespace DTS.Models
{
    public partial class TemplateVersionControll
    {
        public int Id { get; set; }
        public string Template { get; set; }
        public DateTime Date { get; set; }
        public int CreatedBy { get; set; }
        public int TemplateId { get; set; }
        public int StateId { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual TemplateState State { get; set; }
        public virtual Template TemplateNavigation { get; set; }
    }
}
