using AutoTrader.Core.Enums;
using AutoTrader.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Core.Services
{
    public class CacheServiceStub : ICacheService
    {
        public List<ReleaseCategory> Categories { get; set; }
        public List<ReleaseSection> Sections { get; set; }

        public Task ReadCache()
        {
            Categories = new List<ReleaseCategory>
            {
                new ReleaseCategory
                {
                    Name = "Audio",
                    Type = ReleaseCategoryType.Audio,
                    Description = "Audio Category",
                }
            };

            Categories[0].Sections = new List<ReleaseSection>
            {
                new ReleaseSection
                {
                    Name = "Mp3",
                    Description = "Mp3 Section",
                },
                new ReleaseSection
                {
                    Name = "Flac",
                    Description = "Flac Category"
                }
            };

            var mp3SectionRuleSet = new ReleaseSectionRuleSet
            {
                Delimiter = '-'
            };
            var flacSectionRuleSet = new ReleaseSectionRuleSet
            {
                Delimiter = '-'
            };

            Categories[0].Sections[0].RuleSet.Add(mp3SectionRuleSet);
            Categories[0].Sections[1].RuleSet.Add(flacSectionRuleSet);

            return Task.CompletedTask;
        }
    }
}