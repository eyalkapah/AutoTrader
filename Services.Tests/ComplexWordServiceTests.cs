using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
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
    public class ComplexWordServiceTests
    {
        private Mock<ICacheService> _cacheMock;

        [TestMethod]
        public void ShouldGetComplexWord()
        {
            // Arrange
            var sut = new ComplexWordService(_cacheMock.Object);

            // Action
            var result = sut.GetComplexWord("cf8fad5b-d9cb-469f-a165-708677289510");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldGetComplexWords()
        {
            // Arrange
            var sut = new ComplexWordService(_cacheMock.Object);

            // Action
            var result = sut.GetComplexWords();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(UndefinedException))]
        public void ShouldThrowIfComplexWordsNotFound()
        {
            // Arrange
            var service = new ComplexWordService(new CacheService());

            // Action
            var result = service.GetComplexWords();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cacheMock = new Mock<ICacheService>();

            _cacheMock.Setup(c => c.ComplexWords).Returns(new List<ComplexWord>
            {
                new ComplexWord
                {
                     Id = "cf8fad5b-d9cb-469f-a165-708677289510",
                    Name = "Live",
                    Classification =  "SOURCES",
                    Description = "Live regex",
                    WordIds = {
                        "0f8fad5b-d9cb-469f-a165-70867728951e",
                        "0f8fad5b-d9cb-469f-a165-70867728951f",
                        "0f8fad5b-d9cb-469f-a165-70867728951g"
                    }
                }
            });
        }
    }
}