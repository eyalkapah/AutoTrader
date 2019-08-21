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

        [TestMethod]
        public void ShouldConvertToParticipant()
        {
            // Arrange
            var enrollment = _site.Enrollments.Single(e => e.SectionId.Equals("sf8fad5b-d9cb-469f-a165-708677289501"));
            var isAffiliate = enrollment.Affils.Contains("TSP");

            // Action
            var participant = _site.ConvertToParticipant(enrollment, isAffiliate);

            // Assert
            Assert.IsNotNull(participant);
        }

        [TestMethod]
        public void ShouldParticipantParametersEqualToSite()
        {
            // Arrange
            var enrollment = _site.Enrollments.Single(e => e.SectionId.Equals("sf8fad5b-d9cb-469f-a165-708677289501"));
            var isAffiliate = enrollment.Affils.Contains("TSP");

            // Action
            var participant = _site.ConvertToParticipant(enrollment, isAffiliate);

            // Assert
            Assert.AreEqual(_site, participant.Site);
            Assert.AreEqual(enrollment, participant.Enrollment);
            Assert.AreEqual(_site.Logins.Total, participant.Logins.Total);
            Assert.AreEqual(_site.Logins.Download, participant.Logins.Download);
            Assert.AreEqual(_site.Logins.Upload, participant.Logins.Upload);
            Assert.AreEqual(_site.Rank, participant.Rank);
        }

        [TestMethod]
        public void ShouldParticipantRoleBeAffiliate()
        {
        }

        [TestMethod]
        public void ShouldParticipantRoleBeDownloader()
        {
        }

        [TestMethod]
        public void ShouldParticipantRoleBeUploader()
        {
        }

        [TestMethod]
        public void ShouldParticipantRoleBeDownloadAndUploader()
        {
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
                },
                Logins = new Logins
                {
                    Total = 5,
                    Download = 2,
                    Upload = 3
                },
                Enrollments = new List<Enrollment>
                {
                    new Enrollment
                    {
                        Id = "ef8fad5b-d9cb-469f-a165-708677289501",
                        SectionId = "sf8fad5b-d9cb-469f-a165-708677289501",
                        Affils = new List<string>
                        {
                            "TSP",
                            "POW",
                            "ZZZZ",
                            "BPM"
                        },
                        Status = EnrollmentStatus.On,
                        PackagesIds = new List<string>
                        {
                            "rf8fad5b-d9cb-469f-a165-708677289501",
                            "rf8fad5b-d9cb-469f-a165-708677289502",
                            "rf8fad5b-d9cb-469f-a165-708677289503",
                            "rf8fad5b-d9cb-469f-a165-708677289504",
                            "rf8fad5b-d9cb-469f-a165-708677289505"
                        }
                    }
                }
            };
        }
    }
}