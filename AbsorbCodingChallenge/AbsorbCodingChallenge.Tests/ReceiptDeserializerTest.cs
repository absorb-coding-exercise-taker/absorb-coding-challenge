using System;
using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AbsorbCodingChallenge.Tests
{
    [TestClass]
    public class ItemPricesDeserializerTest
    {
        [TestMethod]
        public void ItCreatesAnEmptyItemPricesArray()
        {
            var items = new object[]
            {
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var itemPrices = ItemPricesDeserializer.Create(itemsString);
            Assert.AreEqual(0, itemPrices.Count);
        }

        [TestMethod]
        public void ItCreatesAnItemPricesArray()
        {
            var items = new object[]
            {
                new { Name = "Apples", Price = 1 }
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var itemPrices = ItemPricesDeserializer.Create(itemsString);
            Assert.AreEqual(1, itemPrices.Count);
            Assert.AreEqual("Apples", itemPrices[0].Name);
            Assert.AreEqual(1, itemPrices[0].Price);
        }

        [TestMethod]
        public void ItCreatesABasicReceiptWithBogoPromotion()
        {
            var items = new object[]
            {
                new { Name = "Apples", Price = 2, Promotion = new { Type = "BOGO" } }
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var itemPrices = ItemPricesDeserializer.Create(itemsString);
            Assert.IsInstanceOfType(itemPrices[0].Promotion, typeof(BuyOneGetOneFree));
        }

        [TestMethod]
        public void ItCreatesABasicReceiptWithBogoPercentPromotion()
        {
            var items = new object[]
            {
                new { Name = "Apples", Price = 1, Promotion = new { Type = "BOGO-PERCENT", DiscountPercent = 50 } }
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var itemPrices = ItemPricesDeserializer.Create(itemsString);
            Assert.IsInstanceOfType(itemPrices[0].Promotion, typeof(BuyOneGetOnePercentOff));
            Assert.AreEqual(50, (itemPrices[0].Promotion as BuyOneGetOnePercentOff).DiscountPercent);
        }
        [TestMethod]
        public void ItCreatesABasicReceiptWithMultiBuyPromotion()
        {
            var items = new object[]
            {
                new { Name = "Apples", Price = 4, Promotion = new { Type = "MULTI-BUY", Price = 5, Quantity = 2 } }
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var itemPrices = ItemPricesDeserializer.Create(itemsString);
            Assert.IsInstanceOfType(itemPrices[0].Promotion, typeof(MultiBuy));
            Assert.AreEqual(5, (itemPrices[0].Promotion as MultiBuy).Price);
            Assert.AreEqual(2, (itemPrices[0].Promotion as MultiBuy).Quantity);
        }

    }
}
