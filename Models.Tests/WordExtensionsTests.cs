using AutoTrader.Models.Entities;
using AutoTrader.Models.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class WordExtensionsTests
    {
        private Word _word;

        [TestMethod]
        public void ShouldIgnoreNotSuccess()
        {
            // Arrange
            _word.IgnorePattern = "[-](vls|ep|lp)[-]";

            // Action
            var match = _word.GetMatch("Sean_Taylor-Love_Against_Death-Vinyl-2012-6DM", null);

            // Assert
            Assert.IsTrue(!match.IgnorePattern.Success);
        }

        [TestMethod]
        public void ShouldIgnoreSuccess()
        {
            // Arrange
            _word.IgnorePattern = "[-](vinyl|vls|ep|lp)[-]";

            // Action
            var match = _word.GetMatch("Sean_Taylor-Love_Against_Death-Vinyl-2012-6DM", null);

            // Assert
            Assert.IsTrue(match.IgnorePattern.Success);
        }

        [TestMethod]
        public void ShouldNotGetMatch()
        {
            // Arrange
            _word.Pattern = "[-](vinyl|vls|ep|lp)[-]";

            // Action
            var match = _word.GetMatch("Sean_Taylor-Love_Against_Death-Vainyl-2012-6DM", null);

            // Assert
            Assert.IsTrue(!match.Pattern.Success);
        }

        [TestMethod]
        public void ShouldPatternSuccess()
        {
            // Arrange
            _word.Pattern = "[-](vinyl|vls|ep|lp)[-]";

            // Action
            var match = _word.GetMatch("Sean_Taylor-Love_Against_Death-Vinyl-2012-6DM", null);

            // Assert
            Assert.IsTrue(match.Pattern.Success);
        }

        [TestMethod]
        public void ShouldPatternSuccessWithConstants()
        {
            // Arrange
            var constants = new Dictionary<string, string>
            {
                { "%delimiter%", "-" }
            };

            // Action
            var match = _word.GetMatch("Sean_Taylor-Love_Against_Death-Vinyl-2012-6DM", constants);

            // Assert
            Assert.IsTrue(match.Pattern.Success);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _word = new Word
            {
                Id = "0f8fad5b-d9cb-469f-a165-70867728950m",
                Name = "VINYL",
                Classification = "SOURCES",
                Description = "Vinyl regex",
                Pattern = "[%delimiter%](vinyl|vls|ep|lp)[%delimiter%]",
                IgnorePattern = "(cd)",
                //ForbiddenPattern = ""
            };
        }
    }
}