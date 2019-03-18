using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public class GetTemplateFormQuery : IQuery
    {
        public int TemplateId { get; }

        public GetTemplateFormQuery(int id)
        {
            TemplateId = id;
        }
    }
}
