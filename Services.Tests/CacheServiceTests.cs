using AutoTrader.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class CacheServiceTests
    {
        [TestMethod]
        public async Task ShouldLoadSettings()
        {
            var sut = new CacheService();

            var categories = await sut.GetCategoriesAsync();

            Assert.IsNotNull(categories);
        }
    }
}