using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Shared.Models;
using System;
using System.Linq;

namespace AutoTrader.Services.Services
{
    public static class ContractFactory
    {
        public static Category GetReleaseCategory(CategoryContract category)
        {
            if (category == null)
                return null;

            return new Category
            {
                Name = category.Name,
                Description = category.Description,
                Type = (CategoryType)category.Type,
                Sections = category.Sections.Select(section => GetReleaseSection(section)).ToList()
            };
        }

        public static Section GetReleaseSection(SectionContract section)
        {
            if (section == null)
                return null;

            return new Section
            {
                Name = section.Name,
                Description = section.Description,
                Delimiter = section.Delimiter,
                RuleSet = section.RulesSet.Select(rule => GetReleaseSectionRuleSet(rule)).ToList()
            };

            var ruleSetIds = section
        }

        public static RuleSet GetReleaseSectionRuleSet(RuleSetContract ruleSet)
        {
            if (ruleSet == null)
                return null;

            return new RuleSet
            {
                Delimiter = ruleSet.Delimiter
            };
        }

        internal static RuleSet GetRule(RuleSetContract rule)
        {
            return new RuleSet
            {
                Id = rule.Id,
                Name = rule.Name,
                Type = (RuleSetType)rule.Type,
                Words = rule.Words.Select(w => GetWord(w)).ToList()
            }
        }

        private static Word GetWord(WordContract w)
        {
            return new Word
            {
                Name = w.Name,
                Description = w.Description,
                Classification = w.Classification,
                Permission = (WordPermission)w.Permission,
                Pattern = w.Pattern,
                Ignore = w.Ignore
            };
        }
    }
}