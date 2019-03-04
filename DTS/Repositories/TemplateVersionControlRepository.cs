using DTS.Data;
using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class TemplateVersionControlRepository : RepositoryAsync<TemplateVersionControl>, ITemplateVersionControlRepository
    {
        public TemplateVersionControlRepository(DTSContext DtsContext)
            : base(DtsContext)
        {
        }
    }
}
