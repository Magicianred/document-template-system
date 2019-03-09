using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class TemplateVersion
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int CreatorId { get; set; }
        public int TemplateId { get; set; }
        public int StateId { get; set; }

        public virtual User Creator { get; set; }
        public virtual TemplateState State { get; set; }
        public virtual Template Template { get; set; }
    }
}
