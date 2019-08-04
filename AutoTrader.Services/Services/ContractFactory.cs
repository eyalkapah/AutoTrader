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
                RuleSet = GetRuleSet(section.RulesSet)
            };
        }

        public static RuleSet GetRuleSet(RuleSetContract ruleSetsContract)
        {
            if (ruleSetsContract == null)
                return null;

            return new RuleSet
            {
                Strings = ruleSetsContract.Strings.Select(s => GetStringRuleSet(s)).ToList(),
                Ranges = ruleSetsContract.Ranges.Select(r => GetRangeRuleSet(r)).ToList()
            };
        }

        private static RangeRuleSet GetRangeRuleSet(RangeRuleSetContract rule)
        {
            if (rule == null)
                return null;

            return new RangeRuleSet
            {
                Id = rule.Id,
                Name = rule.Name,
                Description = rule.Description,
                Minimum = rule.Minimum,
                Maxmimum = rule.Maxmimum,
                Type = (RuleSetType)rule.Type,
                Permission = (RuleSetPermission)rule.Permission
            };
        }

        public static StringRuleSet GetStringRuleSet(StringRuleSetContract rule)
        {
            if (rule == null)
                return null;

            return new StringRuleSet
            {
                Id = rule.Id,
                Name = rule.Name,
                Type = (RuleSetType)rule.Type,
                Words = rule.Words.Select(w => GetWord(w)).ToList()
            };
        }

        private static Word GetWord(WordContract w)
        {
            return new Word
            {
                Name = w.Name,
                Description = w.Description,
                Classification = w.Classification,
                Permission = (RuleSetPermission)w.Permission,
                Pattern = w.Pattern,
                Ignore = w.Ignore
            };
        }
    }
}