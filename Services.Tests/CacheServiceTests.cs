using AutoTrader.Models.Entities;
using AutoTrader.Models.Entities.Storage;
using AutoTrader.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class CacheServiceTests
    {
        private AppFile _appFile;

        [TestMethod]
        public async Task ShouldLoadDefaultData()
        {
            // Arrange
            var sut = new CacheService();

            _appFile.FileName = "nofile.json";
            await sut.LoadCacheAsync(_appFile);

            // Assert
            Assert.IsNotNull(sut.Categories, "sample data 'Categories' is null");
            Assert.IsNotNull(sut.Sections, "sample data 'Section' is null");
            Assert.IsTrue(!sut.Sites.Any(), "sample data 'Sites' contains data");
            Assert.IsNotNull(sut.Words, "sample data 'Sites' is null");
            Assert.IsNotNull(sut.ComplexWords, "sample data 'ComplexWords' is null");
            Assert.IsTrue(!sut.PreDbs.Any(), "sample data 'PreDb' contains data");
            Assert.IsNotNull(!sut.Packages.Any(), "sample data 'Packages' is null");
        }

        [TestMethod]
        public async Task ShouldLoadRealData()
        {
            // Arrange
            var sut = new CacheService();
            await sut.LoadCacheAsync(_appFile);

            // Assert
            Assert.IsNotNull(sut.Categories, "real data 'Categories' is null");
            Assert.IsNotNull(sut.Sections, "real data 'Sections' is null");
            Assert.IsNotNull(sut.Sites, "real data 'Sites' is null");
            Assert.IsNotNull(sut.Words, "real data 'Words' is null");
            Assert.IsNotNull(sut.ComplexWords, "real data 'ComplexWords' is null");
            Assert.IsNotNull(sut.PreDbs, "real data 'PreDbs' is null");
            Assert.IsNotNull(sut.Packages, "real data 'Packages' is null");
        }

        [TestMethod]
        public async Task ShouldSaveData()
        {
            _appFile.FileName = "data.json";

            string[] messages =
            {
                "some text",
                "another text"
            };

            // Arrange
            var sut = new CacheService();
            await sut.LoadCacheAsync(_appFile);

            sut.Sections[0].Description = sut.Sections[0].Description == messages[0] ? messages[1] : messages[0];
            var savedMessage = sut.Sections[0].Description;

            await sut.SaveDataAsync();

            await sut.LoadCacheAsync(_appFile);
            Assert.IsTrue(sut.Sections[0].Description == savedMessage);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _appFile = new AppFile
            {
                FileName = "data-sample.json",
                Folder = @"Assets\Data",
                InstallationDefaultFileName = "data-default.json",
                InstallationDefaultFolder = @"Assets\SampleData",
            };
        }
    }
}