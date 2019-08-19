using AutoTrader.Services.Services;
using AutoTrader.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Tests.Entities;
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
    public class JsonHelperTests
    {
        [TestMethod]
        public async Task JsonFileReadTest()
        {
            // Arrange
            var inputFileName = @"TestFiles\sample.json";

            // Action
            var sample = await Task.Run(() => JsonHelper.DeserializeJson<Sample>(inputFileName));

            // Assert
            Assert.IsNotNull(sample);
        }

        [TestMethod]
        public void JsonFileWriteTest()
        {
            // Arrange
            var outputFileName = "output.json";

            if (File.Exists(outputFileName))
                File.Delete(outputFileName);

            var sample = new Sample
            {
                Name = "some data",
                Version = "0.5"
            };

            // Action
            JsonHelper.SerializeJson(sample, outputFileName);

            // Assert
            var isExists = File.Exists(outputFileName);
            Assert.IsTrue(isExists);
        }
    }
}