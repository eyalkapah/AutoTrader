using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class SiteServiceTests
    {
        private Mock<ICacheService> _cacheMock;
        private Mock<ICacheService> _emptyCache;

        [TestMethod]
        public async Task ShouldGetFirstMatchChannelAndBotSite()
        {
            var sut = new SiteService(_cacheMock.Object);

            var site = await sut.GetSiteAsync("test_channel", "test_bot");

            Assert.AreEqual(site.Id, "A");
        }

        [TestMethod]
        public async Task ShouldGetSite()
        {
            var sut = new SiteService(_cacheMock.Object);

            var sites = await sut.GetSitesAsync();

            Assert.IsNotNull(sites);
        }

        [TestMethod]
        public async Task ShouldNoMatchChannelAndBotSite()
        {
            var sut = new SiteService(_cacheMock.Object);

            var site = await sut.GetSiteAsync("test_channel", "eyalk");

            Assert.IsNull(site);
        }

        [TestMethod]
        public async Task ShouldNotGetSites()
        {
            var sut = new SiteService(_emptyCache.Object);

            var sites = await sut.GetSitesAsync();

            Assert.IsNull(sites);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cacheMock = new Mock<ICacheService>();
            _emptyCache = new Mock<ICacheService>();

            _cacheMock.Setup(c => c.GetSitesAsync()).Returns(
                Task.FromResult(new List<Site>
                {
                    new Site
                    {
                        Id = "A",
                        IrcInfo = new List<SiteIrcInfo>
                        {
                            new SiteIrcInfo
                            {
                                Channel = "test_channel",
                                Bot = "test_bot"
                            }
                        }
                    },
                    new Site
                    {
                        Id = "B",
                        IrcInfo = new List<SiteIrcInfo>
                        {
                            new SiteIrcInfo
                            {
                                Channel = "test_channel",
                                Bot = "test_bot"
                            }
                        }
                    },
                    new Site
                    {
                        Id = "C",
                        IrcInfo = new List<SiteIrcInfo>
                        {
                            new SiteIrcInfo
                            {
                                Channel = "another_test_channel",
                                Bot = "another_test_bot"
                            }
                        }
                    }
                }));
        }
    }
}