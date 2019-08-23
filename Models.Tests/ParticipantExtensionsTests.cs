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
    public class ParticipantExtensionsTests
    {
        private List<Participant> _participants;

        [TestMethod]
        public void ShouldGetTopRatedAffiliate()
        {
            // Action
            var p = _participants.GetTopRatedSite(new[] { ParticipantRole.Affiliate });

            // Assert
            Assert.AreEqual(p.Site.Id, "9");
        }

        [TestMethod]
        public void ShouldGetTopRatedDownloader()
        {
            // Action
            var p = _participants.GetTopRatedSite(new[] { ParticipantRole.Downloader });

            // Assert
            Assert.AreEqual(p.Site.Id, "5");
        }

        [TestMethod]
        public void ShouldGetTopRatedUploader()
        {
            // Arrange

            // Action
            var p = _participants.GetTopRatedSite(new[] { ParticipantRole.Uploader });

            // Assert
            Assert.AreEqual(p.Site.Id, "8");
        }

        [TestMethod]
        public void ShouldGetTopRatedUploaderAndDownloader()
        {
            // Action
            var p = _participants.GetTopRatedSite(new[] { ParticipantRole.UploaderAndDownloader });

            // Assert
            Assert.AreEqual(p.Site.Id, "1");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _participants = new List<Participant>
            {
                new Participant
                {
                    Site = new Site
                    {
                        Id = "1",
                        Name = "AA"
                    },
                    Rank = 10,
                    Role = ParticipantRole.UploaderAndDownloader
                },
                new Participant
                {
                    Site = new Site
                    {
                        Id = "2",
                        Name = "BB"
                    },
                    Rank = 9,
                    Role = ParticipantRole.UploaderAndDownloader
                },
                new Participant
                {
                    Site = new Site
                    {
                        Id = "3",
                        Name = "CC"
                    },
                    Rank = 8,
                    Role = ParticipantRole.Affiliate
                },

                new Participant
                {
                    Site = new Site
                    {
                        Id = "4",
                        Name = "DD"
                    },
                    Rank = 7,
                    Role = ParticipantRole.Uploader
                },

                new Participant
                {
                    Site = new Site
                    {
                        Id = "5",
                        Name = "EE"
                    },
                    Rank = 9,
                    Role = ParticipantRole.Downloader
                },
                new Participant
                {
                    Site = new Site
                    {
                        Id = "6",
                        Name = "FF"
                    },
                    Rank = 6,
                    Role = ParticipantRole.Uploader
                },
                new Participant
                {
                    Site = new Site
                    {
                        Id = "7",
                        Name = "GG"
                    },
                    Rank = 8,
                    Role = ParticipantRole.UploaderAndDownloader
                },
                new Participant
                {
                    Site = new Site
                    {
                        Id = "8",
                        Name = "HH"
                    },
                    Rank = 9,
                    Role = ParticipantRole.Uploader
                },
                new Participant
                {
                    Site = new Site
                    {
                        Id = "9",
                        Name = "II"
                    },
                    Rank = 10,
                    Role = ParticipantRole.Affiliate
                },
            };
        }
    }
}