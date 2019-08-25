using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
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
    public class PackageExtensionsTests
    {
        private List<ComplexWord> _complexWords;
        private Dictionary<string, string> _constants;
        private Package _packageWithBan;
        private Package _packageWithMust;
        private List<Word> _words;

        [TestMethod]
        public void ShouldBanPackage()
        {
            // Action
            var isValid = _packageWithBan.IsPackageValid(_words, new List<ComplexWord>(), "Treisor_Luke-Internal-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(!isValid);
        }

        [TestMethod]
        public void ShouldBanPackageWithComplexWord()
        {
            // Arrange
            _packageWithBan.WordId = _complexWords[0].Id;

            // Action
            var isValid = _packageWithBan.IsPackageValid(_words, _complexWords, "Treisor_Luke-FM-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(!isValid);
        }

        [TestMethod]
        public void ShouldMustPackage()
        {
            // Action
            var isValid = _packageWithMust.IsPackageValid(_words, new List<ComplexWord>(), "Treisor_Luke-Internal-WEB-2019-KLIN", _constants);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ShouldMustPackageWithComplexWord()
        {
            // Arrange
            _packageWithMust.WordId = _complexWords[0].Id;

            // Action
            var isValid = _packageWithMust.IsPackageValid(_words, _complexWords, "Treisor_Luke-SAT-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ShouldNotBanPackageBecauseIgnoreDefined()
        {
            var word = _words.FirstOrDefault(w => w.Id == "W1");
            word.IgnorePattern = "-Int-";

            // Action
            var isValid = _packageWithBan.IsPackageValid(_words, new List<ComplexWord>(), "Treisor_Luke-Int-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ShouldNotBanPackageWithComplexWord()
        {
            // Arrange
            _packageWithBan.WordId = _complexWords[0].Id;

            // Action
            var isValid = _packageWithBan.IsPackageValid(_words, _complexWords, "Treisor_Luke-Internal-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ShouldNotMustPackage()
        {
            // Action
            var isValid = _packageWithMust.IsPackageValid(_words, new List<ComplexWord>(), "Treisor_Luke-Internal-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(!isValid);
        }

        [TestMethod]
        public void ShouldPackageNotBeValidForBannedApplicability()
        {
            // Action
            var isValid = _packageWithBan.IsPackageValid(_words, new List<ComplexWord>(), "Treisor_Luke-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownWordException))]
        public void ShouldThrowExceptionForNoWordsDefined()
        {
            // Arrange
            _packageWithBan.WordId = "NoWord";

            // Action
            _packageWithBan.IsPackageValid(_words, new List<ComplexWord>(), "Treisor_Luke-WEB-2016-KLIN", _constants);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownWordException))]
        public void ShouldThrowExceptionForUnknownWord()
        {
            // Arrange
            _packageWithBan.WordId = "NoWord";

            // Action
            _packageWithBan.IsPackageValid(null, new List<ComplexWord>(), "Treisor_Luke-WEB-2016-KLIN", _constants);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _packageWithBan = new Package
            {
                Id = "P1",
                Applicability = PackageApplicability.Banned,
                Name = "Ban Internal Resources",
                WordId = "W1"
            };

            _packageWithMust = new Package
            {
                Id = "P2",
                Applicability = PackageApplicability.Must,
                Name = "Only Current Year",
                WordId = "W3"
            };

            _words = new List<Word>
            {
                new Word
                {
                    Id = "W1",
                    Name = "Internal",
                    Classification = "INTERNAL",
                    Description = "Describes if released for internal purpose",
                    Pattern = "([-](INT|INTERNAL)[-])"
                },
                new Word
                {
                    Id = "W2",
                    Name = "3 Max CDs",
                    Classification = "MAX CDS",
                    Description = "More than 3 CDs",
                    Pattern = "[-\\(](([4-9])|([1-9][0-9])+)CDS?[-\\)]"
                },
                new Word
                {
                    Id = "W3",
                    Name = "CurrentYear",
                    Classification = "YEAR",
                    Description = "Current year",
                    Pattern = "[%delimiter%](%current_year%)[%delimiter%]"
                },
                new Word
                {
                    Id = "0f8fad5b-d9cb-469f-a165-70867728951e",
                    Name = "FRESHFM",
                    Classification = "SOURCES",
                    Description = "FreshFM regex",
                    Pattern = "[%delimiter%](freshfm)[%delimiter%]"
                },
                new Word
                {
                    Id = "0f8fad5b-d9cb-469f-a165-70867728951g",
                    Name = "SAT",
                    Classification = "SOURCES",
                    Description = "SAT regex",
                    Pattern = "[%delimiter%](SAT)[%delimiter%]"
                },
                new Word
                {
                    Id = "0f8fad5b-d9cb-469f-a165-70867728951f",
                    Name = "FM",
                    Classification = "SOURCES",
                    Description = "FM regex",
                    Pattern = "[%delimiter%](FM)[%delimiter%]"
                }
            };

            _complexWords = new List<ComplexWord>
            {
                new ComplexWord
                {
                    Id = "cf8fad5b-d9cb-469f-a165-708677289510",
                    Classification = "SOURCES",
                    Description = "Live Regex",
                    Name = "Live",
                    WordIds = new List<string>
                    {
                        "0f8fad5b-d9cb-469f-a165-70867728951e",
                        "0f8fad5b-d9cb-469f-a165-70867728951f",
                        "0f8fad5b-d9cb-469f-a165-70867728951g"
                    }
                }
            };

            _constants = new Dictionary<string, string>
            {
                { "%delimiter%", "-" },
                { "%current_year%", DateTime.Now.Year.ToString() }
            };
        }
    }
}