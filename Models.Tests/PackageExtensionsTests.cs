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
        private Package _packageWithBan;
        private Package _packageWithMust;
        private List<Word> _words;
        private Dictionary<string, string> _constants;

        [TestMethod]
        public void ShouldMustPackage()
        {
            // Action
            var isValid = _packageWithMust.IsPackageValid(_words, "Treisor_Luke-Internal-WEB-2019-KLIN", _constants);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ShouldNotMustPackage()
        {
            // Action
            var isValid = _packageWithMust.IsPackageValid(_words, "Treisor_Luke-Internal-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(!isValid);
        }

        [TestMethod]
        public void ShouldBanPackage()
        {
            // Action
            var isValid = _packageWithBan.IsPackageValid(_words, "Treisor_Luke-Internal-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(!isValid);
        }

        [TestMethod]
        public void ShouldNotBanPackageBecauseIgnoreDefined()
        {
            var word = _words.FirstOrDefault(w => w.Id == "W1");
            word.IgnorePattern = "-Int-";

            // Action
            var isValid = _packageWithBan.IsPackageValid(_words, "Treisor_Luke-Int-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ShouldPackageNotBeValidForBannedApplicability()
        {
            // Action
            var isValid = _packageWithBan.IsPackageValid(_words, "Treisor_Luke-WEB-2016-KLIN", _constants);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidWordException))]
        public void ShouldThrowExceptionForNoWordsDefined()
        {
            // Arrange
            _packageWithBan.WordId = "NoWord";

            // Action
            _packageWithBan.IsPackageValid(_words, "Treisor_Luke-WEB-2016-KLIN", _constants);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidWordException))]
        public void ShouldThrowExceptionForUnknownWord()
        {
            // Arrange
            _packageWithBan.WordId = "NoWord";

            // Action
            _packageWithBan.IsPackageValid(null, "Treisor_Luke-WEB-2016-KLIN", _constants);
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