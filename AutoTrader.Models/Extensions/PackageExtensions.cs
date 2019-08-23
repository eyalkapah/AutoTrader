using AutoTrader.Models.Entities;
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
        public static bool IsPackageValid(this Package package, List<Word> words, string text)
        {
            // Handle a WORD only
            var word = words?.FirstOrDefault(w => w.Id.Equals(package.WordId));

            if (word == null)
                throw new InvalidWordException(package.Id, package.WordId);

            var result = word.GetMatch(text);

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