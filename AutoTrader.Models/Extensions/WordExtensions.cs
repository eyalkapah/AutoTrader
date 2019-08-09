using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class WordExtensions
    {
        public static WordMatchResult GetMatch(this Word word, string text, Dictionary<string, string> constants)
        {
            return new WordMatchResult
            {
                Pattern = DoMatch(text, constants, word.Pattern),
                IgnorePattern = DoMatch(text, constants, word.IgnorePattern),
            };
        }

        public static WordMatchResult GetMatch(this Word word, string text)
        {
            return GetMatch(word, text, null);
        }

        private static Match DoMatch(string text, Dictionary<string, string> constants, string pattern)
        {
            if (constants != null)
            {
                foreach (var key in constants.Keys)
                    pattern = Regex.Replace(pattern, key, constants[key]);
            }

            return Regex.Match(text, pattern, RegexOptions.IgnoreCase);
        }
    }
}