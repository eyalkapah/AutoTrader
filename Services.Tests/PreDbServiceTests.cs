using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class PreDbServiceTests
    {
        private Mock<ICacheService> _cacheMock;
        private PreDbService _preDbService;

        [TestMethod]
        public void ShouldGetPreDb()
        {
            // Action
            var predB = _preDbService.GetPreDb("PDB-Channel1", "PDB-BOT1");

            // Assert
            Assert.IsNotNull(predB);
        }

        [TestMethod]
        public void ShouldNotGetPreDb()
        {
            // Action
            var predB = _preDbService.GetPreDb("PDB-Channel1", "PDB-BOT2");

            // Assert
            Assert.IsNull(predB);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cacheMock = new Mock<ICacheService>();

            _cacheMock.Setup(c => c.PreDbs).Returns(new List<PreDb>
            {
                new PreDb
                {
                    Id = "PD1",
                    Bot = "PDB-BOT1",
                    Channel = "PDB-Channel1",
                    Name = "PDB1"
                },
                new PreDb
                {
                    Id = "PD2",
                    Bot = "PDB-BOT2",
                    Channel = "PDB-Channel2",
                    Name = "PDB2"
                },
            });

            _preDbService = new PreDbService(_cacheMock.Object);
        }
    }
}