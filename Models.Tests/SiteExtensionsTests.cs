using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tests
{
    [TestClass]
    public class SiteExtensionsTests
    {
        public Site _site { get; private set; }

        [TestMethod]
        public void ShouldNotSiteHaveSiteInfo()
        {
            // Arrange
            var result = _site.IsIrcInfo("pf-spam", "pf-bot1");

            // Assert
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void ShouldSiteHaveSiteInfoNoPound()
        {
            // Arrange
            var result = _site.IsIrcInfo("pf-spam", "pf-bot");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShouldSiteHaveSiteInfoWithPount()
        {
            // Arrange
            var result = _site.IsIrcInfo("#pf-spam", "pf-bot");

            // Assert
            Assert.IsTrue(result);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _site = new Site
            {
                Id = "sitead5b-d9cb-469f-a165-708677289501",
                Name = "A-Site",
                Status = SiteStatus.On,
                Rank = 6,
                IrcInfo = new List<SiteIrcInfo>
                {
                    new SiteIrcInfo
                    {
                        Channel = "pf-spam",
                        Bot = "pf-bot"
                    },
                    new SiteIrcInfo
                    {
                        Channel = "#puzzlefactory",
                        Bot = "PiEcE"
                    }
                }
            };
        }
    }
}