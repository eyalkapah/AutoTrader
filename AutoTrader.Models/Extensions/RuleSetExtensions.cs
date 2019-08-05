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
        public static RuleSetResult ProcessRuleSet(this StringRuleSet stringRuleSet, string releaseName)
        {
            var result = new RuleSetResult { RuleSet = stringRuleSet };

            var words = new ConcurrentBag<string>();

            Parallel.ForEach(stringRuleSet.Words, word =>
            {
                var match = Regex.Match(releaseName, word.Pattern, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    // If no Ignore words for this banned word -> Invalid !
                    if (string.IsNullOrEmpty(word.Ignore))
                        words.Add(word.Name);

                    // If ignore word wasn't appeared -> Invalid !
                    else
                    {
                        match = Regex.Match(releaseName, word.Ignore, RegexOptions.IgnoreCase);

                        if (!match.Success)
                            words.Add(word.Name);
                    }
                }
            });

            result.Matches = words.ToList();
            return result;
        }
    }
}