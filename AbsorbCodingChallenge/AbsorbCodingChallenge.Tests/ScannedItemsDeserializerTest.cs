using System;
using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbsorbCodingChallenge.Tests
{
    [TestClass]
    public class ScannedItemsDeserializerTest
    {
        [TestMethod]
        public void ItCreatesAnEmptyList()
        {
            var items = ScannedItemsDeserializer.Create(new string[] { });
            Assert.AreEqual(0, items.Count);
        }

        [TestMethod]
        public void ItCreatesAScannedItem()
        {
            var items = ScannedItemsDeserializer.Create(new string[] { "Apple" });
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("Apple", items[0].Name);
        }
    }
}
