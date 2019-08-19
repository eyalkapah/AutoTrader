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
            // Arrange
            var sut = new CacheService();
            await sut.LoadCacheAsync(null);

            var categories = sut.Categories;

            // Assert
            Assert.IsNotNull(categories);
        }
    }
}