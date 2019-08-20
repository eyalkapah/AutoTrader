using AutoTrader.Models.Entities;
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
    public class ReleaseExtensionsTests
    {
        private AudioRelease _release;

        [TestMethod]
        public void ShouldExtractArtistAndTitle()
        {
            // Action
            _release.ExtractArtistAndTitle('-');

            // Assert
            Assert.AreEqual(_release.Artist, "Vinnie_Great");
            Assert.AreEqual(_release.Title, "Almost_Dancing");
        }

        [TestMethod]
        public void ShouldExtractGroup()
        {
            // Action
            _release.ExtractGroup();

            // Assert
            Assert.AreEqual(_release.Group, "KLIN");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _release = new AudioRelease("Vinnie_Great-Almost_Dancing-SINGLE-WEB-2019-KLIN");
        }
    }
}