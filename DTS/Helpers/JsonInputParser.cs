﻿using DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Helpers
{
    public class JsonInputParser
    {
        public string FillTemplateFromJson(object data, TemplateVersion template)
        {
            var userInput = new Dictionary<string, string>();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(data), userInput);

            foreach (var input in userInput)
            {
                template.Content = template.Content.Replace(input.Key, input.Value);
            }

            return template.Content;
        }
    }
}
