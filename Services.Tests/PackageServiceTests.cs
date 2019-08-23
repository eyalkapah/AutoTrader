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
    public class PackageServiceTests
    {
        private Mock<ICacheService> _cacheMock;
        private WordService _wordService;

        [TestMethod]
        public void PackageShouldBeValid()
        {
            // Arrange
            var packageService = new PackageService(_cacheMock.Object, _wordService);

            // Action
            var result = packageService.IsPackageValid("P1", "Treisor_Luke-WEB-2016-KLIN", null);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPackageException))]
        public void ShouldThrowIfPackageNotFound()
        {
            // Arrange
            var packageService = new PackageService(_cacheMock.Object, _wordService);

            // Action
            var result = packageService.IsPackageValid("P2", "Treisor_Luke-WEB-2016-KLIN", null);
        }

        [TestMethod]
        [ExpectedException(typeof(UndefinedPackagesException))]
        public void ShouldThrowIfNoPackagesFound()
        {
            // Arrange
            var cacheEmpty = new Mock<ICacheService>();
            var packageService = new PackageService(cacheEmpty.Object, _wordService);

            // Action
            var result = packageService.GetPackages();
        }

        [TestMethod]
        public void ShouldGetPackages()
        {
            // Arrange
            var packageService = new PackageService(_cacheMock.Object, null);

            // Action
            var packages = packageService.GetPackages();

            // Assert
            Assert.IsNotNull(packages);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cacheMock = new Mock<ICacheService>();

            _cacheMock.Setup(p => p.Packages).Returns(
                new List<Package>
                {
                    new Package
                    {
                        Id = "P1",
                        Applicability = PackageApplicability.Banned,
                        Name = "Ban Web Resources",
                        WordId = "W1"
                    }
            });

            _cacheMock.Setup(w => w.Words).Returns(
                new List<Word>
                {
                    new Word
                    {
                        Id = "W1",
                        Classification = "SOURCES",
                        Pattern = "[%delimiter%](web|freeweb)[%delimiter%]",
                        Name = "WEB"
                    }
                });

            _wordService = new WordService(_cacheMock.Object);
        }
    }
}