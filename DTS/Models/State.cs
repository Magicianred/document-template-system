﻿using System;
using System.Collections.Generic;

namespace DTS.Models
{
    public partial class State
    {
        public State()
        {
            Template = new HashSet<Template>();
            TemplateVersionControll = new HashSet<TemplateVersionControll>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Template> Template { get; set; }
        public virtual ICollection<TemplateVersionControll> TemplateVersionControll { get; set; }
    }
}
