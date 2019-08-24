﻿using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class PackageExtensions
    {
        public static bool IsPackageValid(this Package package, List<Word> words, IEnumerable<ComplexWord> complexWords, string text, Dictionary<string, string> contatns)
        {
            // maybe it's a complex word
            var complexWord = complexWords.FirstOrDefault(w => w.Id.Equals(package.WordId));

            if (complexWord != null)
            {
                foreach (var wordId in complexWord.WordIds)
                {
                    if (!IsValidWord(package, words, text, contatns))
                        return false;
                }

                return true;
            }

            return IsValidWord(package, words, text, contatns);
        }

        private static bool IsValidWord(Package package, List<Word> words, string text, Dictionary<string, string> contatns)
        {
            // Handle a WORD only
            var word = words?.FirstOrDefault(w => w.Id.Equals(package.WordId));

            if (word == null)
                throw new InvalidWordException(package.Id, package.WordId);

            var result = word.GetMatch(text, contatns);

            switch (package.Applicability)
            {
                case PackageApplicability.Must:
                    return result.Pattern.Success && !result.IgnorePattern.Success;

                case PackageApplicability.Banned:
                    return !result.Pattern.Success || result.Pattern.Success && result.IgnorePattern.Success;

                default:
                    throw new NotImplementedException("Unknown applicability");
            }
        }
    }
}