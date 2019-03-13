using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.APi.Models
{
    public class TemplateVersionInput
    {
        public int AuthorId { get; set; }
        public string TemplateName { get; set; }
        public string Template { get; set; }
    }
}
