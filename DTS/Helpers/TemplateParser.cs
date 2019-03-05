using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTS.Helpers
{
    public class TemplateParser
    {
        private const string StartPattern = "(&lt;*)";
        private const string EndPattern = "(&gt;*)";
        private const string TemplateFieldsPattern = "&lt;([#@])([/sA-Za-z_-]*)&gt;";

        public Dictionary<string, string> ParseFields(string formBase)
        {
            

            MatchCollection matches = Regex.Matches(formBase, TemplateFieldsPattern);

            var userMatchMap = new Dictionary<string, string>();

            foreach (var match in matches)
            {
                  
                var replacement = "";
                Regex beginningRegex = new Regex(StartPattern);
                Regex endingRegex = new Regex(EndPattern);

                var valueWithBeginningCleared = beginningRegex.Replace(match.ToString(), replacement);

                var finalValue = endingRegex.Replace(valueWithBeginningCleared, replacement);
                userMatchMap.TryAdd(match.ToString(), finalValue);
            }

            return userMatchMap;
        }
    }
}
