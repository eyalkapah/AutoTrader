using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
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
    public class SectionServiceTests
    {
        private Mock<ICacheService> _cacheMock;
        private Mock<IPackageService> _packagesServiceMock;
        private Mock<IComplexWordService> _complexWordServiceMock;

        [TestMethod]
        public void ShouldGetSection()
        {
            // Arrange
            var wordService = new WordService(_cacheMock.Object);
            var packageService = new PackageService(_cacheMock.Object, wordService, _complexWordServiceMock.Object);
            var sectionService = new SectionService(_cacheMock.Object, packageService);

            // Action
            var section = sectionService.GetSection("MP3");

            // Assert
            Assert.IsNotNull(section);
        }

        [TestMethod]
        public void ShouldGetSections()
        {
            // Arrange
            var sectionService = new SectionService(_cacheMock.Object, null);

            // Actions
            var sections = sectionService.GetSections();

            // Assert
            Assert.IsNotNull(sections);
        }

        [TestMethod]
        [ExpectedException(typeof(UndefinedException))]
        public void ShouldThrowIfSectionsNotFound()
        {
            // Arrange
            var sectionService = new SectionService(new CacheService(), null);

            // Action
            var result = sectionService.GetSections();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cacheMock = new Mock<ICacheService>();
            _packagesServiceMock = new Mock<IPackageService>();
            _complexWordServiceMock = new Mock<IComplexWordService>();

            _cacheMock.Setup(c => c.Sections).Returns(
                new List<Section>
                {
                new Section
                {
                   Id = "S1",
                   Name = "Mp3",
                   Description = "Mp3 Section",
                   PackageId = "Mp3PackageId"
                },
                new Section
                {
                    Id = "S2",
                    Name = "Flac",
                    Description = "Flac Section",
                    PackageId = "FlacPackageId"
                }
            });

            _cacheMock.Setup(p => p.Packages).Returns(
                new List<Package>
                {
                    new Package
                    {
                        Id = "Mp3PackageId",
                        Applicability = PackageApplicability.Must,
                        Name = "Mp3 Section Package",
                        WordId = "W1"
                    }
            });

            _cacheMock.Setup(w => w.Words).Returns(
                new List<Word>
                {
                    new Word
                    {
                        Id = "W1",
                        Classification = "SECTION",
                        Pattern = "(MP3)",
                        Name = "MP3"
                    }
                });
        }
    }
}