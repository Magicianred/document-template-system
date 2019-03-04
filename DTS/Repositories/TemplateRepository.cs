using DTS.Data;
using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class TemplateRepository : RepositoryAsync<Template>, ITemplateRepository
    {
        public TemplateRepository(DTSContext DtsContext)
            : base(DtsContext)
        {
        }
    }
}
