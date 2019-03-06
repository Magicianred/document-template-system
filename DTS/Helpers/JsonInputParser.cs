using DTS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Helpers
{
    public class JsonInputParser
    {
        public string FillTemplateFromJson(object data, TemplateVersionControl template)
        {
            var userInput = new Dictionary<string, string>();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(data), userInput);

            foreach (var input in userInput)
            {
                template.TemplateVersion = template.TemplateVersion.Replace(input.Key, input.Value);
            }

            return template.TemplateVersion;
        }
    }
}
