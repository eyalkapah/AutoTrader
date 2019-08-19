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
        public void ShouldGetFirstMatchChannelAndBotSite()
        {
            var sut = new SiteService(_cacheMock.Object);

            var site = sut.GetSite("test_channel", "test_bot");

            Assert.AreEqual(site.Id, "A");
        }

        [TestMethod]
        public void ShouldGetSite()
        {
            var sut = new SiteService(_cacheMock.Object);

            var sites = sut.GetSites();

            Assert.IsNotNull(sites);
        }

        [TestMethod]
        public void ShouldNoMatchChannelAndBotSite()
        {
            var sut = new SiteService(_cacheMock.Object);

            var site = sut.GetSite("test_channel", "eyalk");

            Assert.IsNull(site);
        }

        [TestMethod]
        public void ShouldNotGetSites()
        {
            var sut = new SiteService(_emptyCache.Object);

            var sites = sut.GetSites();

            Assert.IsNull(sites);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cacheMock = new Mock<ICacheService>();
            _emptyCache = new Mock<ICacheService>();

            _cacheMock.Setup(c => c.Sites).Returns(
                new List<Site>
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
                });
        }
    }
}