using AutoTrader.Core.Enums;
using AutoTrader.Core.Services;
using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class CacheServiceStub : ICacheService
    {
        public List<Category> Categories { get; set; }
        public List<Section> Sections { get; set; }

        public Section GetSection(string name)
        {
            if (Sections == null)
                return null;

            return Sections.FirstOrDefault(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public Task ReadCache()
        {
            Categories = new List<Category>
            {
                new Category
                {
                    Name = "Audio",
                    Type = CategoryType.Audio,
                    Description = "Audio Category",
                }
            };

            Categories[0].Sections = new List<Section>
            {
                new Section
                {
                    Name = "Mp3",
                    Description = "Mp3 Section",
                },
                new Section
                {
                    Name = "Flac",
                    Description = "Flac Category"
                }
            };

            var mp3SectionRuleSet = new RuleSet
            {
                Delimiter = '-'
            };
            var flacSectionRuleSet = new RuleSet
            {
                Delimiter = '-'
            };

            Categories[0].Sections[0].RuleSet.Add(mp3SectionRuleSet);
            Categories[0].Sections[1].RuleSet.Add(flacSectionRuleSet);

            return Task.CompletedTask;
        }
    }
}