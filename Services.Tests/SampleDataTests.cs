using AutoTrader.Services.Services;
using AutoTrader.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class SampleDataTests
    {
        private string _dataFile;

        [TestMethod]
        public async Task JsonFileReadTest()
        {
            // Arrange
            var inputFileName = $@"TestFiles\{_dataFile}";

            // Action
            var sample = await Task.Run(() => JsonHelper.DeserializeJson<DataContract>(inputFileName));

            // Assert
            Assert.IsNotNull(sample);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _dataFile = "data.json";
        }
    }
}