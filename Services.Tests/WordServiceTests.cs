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
    public class WordServiceTests
    {
        private Mock<ICacheService> _cacheMock;
        private Mock<ICacheService> _emptyCache;

        [TestMethod]
        public void ShouldGetWord()
        {
            // Arrange
            var sut = new WordService(_cacheMock.Object);

            // Action
            var word = sut.GetWord("0f8fad5b-d9cb-469f-a165-70867728950m");

            // Assert
            Assert.IsNotNull(word);
        }

        [TestMethod]
        public void ShouldGetWords()
        {
            // Arrange
            var sut = new WordService(_cacheMock.Object);

            // Action
            var words = sut.GetWords();

            // Assert
            Assert.IsNotNull(words);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cacheMock = new Mock<ICacheService>();

            _cacheMock.Setup(c => c.Words).Returns(new List<Word>
            {
                new Word
                {
                    Id = "0f8fad5b-d9cb-469f-a165-70867728950m",
                    Name = "VINYL",
                    Classification = "SOURCES",
                    Description = "Vinyl regex",
                    Pattern = "[%delimiter%](vinyl|vls|ep|lp)[%delimiter%]",
                    IgnorePattern = "",
                }
            });
        }
    }
}