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
            var assembly = Assembly.GetExecutingAssembly();
            var folder = Path.GetDirectoryName(assembly.Location);
            var path = Path.Combine(folder, @"TestFiles\data.json");

            // Action
            var sample = await Task.Run(() => JsonHelper.DeserializeJson<Sample>(path));

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

        //[TestMethod]
        //public async Task ShouldHaveJson()
        //{
        //    var assembly = Assembly.GetExecutingAssembly();

        //    var folder = Path.GetDirectoryName(assembly.Location);

        //    var json = File.ReadAllText(@"TestFiles\data.json");
        //    var path = Path.Combine(folder, @"TestFiles\data.json");

        //    var sut = await JsonHelper.DeserializeAsync<DataContract>(path);

        //    //Assert.IsTrue(File.Exists(myfile), "Deployment failed: {0} did not get deployed.", myfile);
        //}

        [TestInitialize]
        public void TestInitialize()
        {
        }
    }
}