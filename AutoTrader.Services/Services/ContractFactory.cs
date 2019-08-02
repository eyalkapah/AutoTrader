using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;
using AutoTrader.Shared.Models;
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
                RuleSet = section.RuleSet.Select(rule => GetReleaseSectionRuleSet(rule)).ToList()
            };
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
    }
}