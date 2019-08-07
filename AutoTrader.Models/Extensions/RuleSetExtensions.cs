using AutoTrader.Models.Entities.RuleSetEntities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class RuleSetExtensions
    {
        public static RuleSetResult ProcessStringRuleSet(this StringRuleSet stringRuleSet, string releaseName, Dictionary<string, string> constants)
        {
            var result = new RuleSetResult { RuleSet = stringRuleSet };

            var words = new ConcurrentBag<string>();

            Parallel.ForEach(stringRuleSet.Words, word =>
            {
                var match = DoMatch(releaseName, constants, word.Pattern);

                if (match.Success)
                {
                    // If no Ignore words for this banned word -> Invalid !
                    if (string.IsNullOrEmpty(word.IgnorePattern))
                        words.Add(word.Name);

                    // If ignore word wasn't appeared -> Invalid !
                    else
                    {
                        match = DoMatch(releaseName, constants, word.IgnorePattern);

                        if (!match.Success)
                            words.Add(word.Name);
                    }
                }
            });

            result.Matches = words.ToList();
            return result;
        }

        private static Match DoMatch(string releaseName, Dictionary<string, string> constants, string originalWord)
        {
            var pattern = originalWord;

            foreach (var key in constants.Keys)
                pattern = Regex.Replace(pattern, key, constants[key]);

            return Regex.Match(releaseName, pattern, RegexOptions.IgnoreCase);
        }
    }
}