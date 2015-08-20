using System;
using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbsorbCodingChallenge.Tests
{
    [TestClass]
    public class StoreTest
    {
        [TestMethod]
        public void ItCreatesAStoreFromItems()
        {
            var store = new Store();
            store.AddItemPrices(new ItemPrice() { Name = "Apple", Price = 1 });
            var receipt = store.Checkout(new ScannedItem { Name = "Apple" });
            Assert.AreEqual(1, receipt.GetItems().Count);
            Assert.AreEqual(1, receipt.GetItems()[0].Price);
        }
        [TestMethod]
        public void ItOverwritesNewItemPricesBetweenCheckouts()
        {
            var store = new Store();
            store.AddItemPrices(new ItemPrice() { Name = "Apple", Price = 1 });
            store.Checkout(new ScannedItem { Name = "Apple" });
            store.AddItemPrices(new ItemPrice() { Name = "Apple", Price = 2 });
            var receipt2 = store.Checkout(new ScannedItem { Name = "Apple" });
            Assert.AreEqual(2, receipt2.GetItems()[0].Price);
        }

    }
}
