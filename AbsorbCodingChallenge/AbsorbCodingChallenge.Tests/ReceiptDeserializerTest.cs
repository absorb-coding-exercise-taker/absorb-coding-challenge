using System;
using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AbsorbCodingChallenge.Tests
{
    [TestClass]
    public class ReceiptDeserializerTest
    {
        [TestMethod]
        public void ItCreatesABasicReceipt()
        {
            var items = new object[]
            {
                new { Name = "Apples", Price = 1 }
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var receipt = ReceiptDeserializer.Create(itemsString, "Apples");
            var value = receipt.GetItems();
            Assert.AreEqual(1, value.Count);
            Assert.AreEqual("Apples", value[0].Name);
            Assert.AreEqual(1, value[0].Price);
        }
        [TestMethod]
        public void ItCreatesABasicReceiptWithBogoPromotion()
        {
            var items = new object[]
            {
                new { Name = "Apples", Price = 2, Promotion = new { Type = "BOGO" } }
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var receipt = ReceiptDeserializer.Create(itemsString, "Apples", "Apples");
            var value = receipt.GetItems();
            Assert.AreEqual(1, value.Count);
            Assert.AreEqual("Apples", value[0].Name);
            Assert.AreEqual(2, value[0].Price);
        }
        [TestMethod]
        public void ItCreatesABasicReceiptWithBogoPercentPromotion()
        {
            var items = new object[]
            {
                new { Name = "Apples", Price = 1, Promotion = new { Type = "BOGO-PERCENT", DiscountPercent = 50 } }
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var receipt = ReceiptDeserializer.Create(itemsString, "Apples", "Apples");
            var value = receipt.GetItems();
            Assert.AreEqual(1.50M, value[0].Price);
        }
        [TestMethod]
        public void ItCreatesABasicReceiptWithMultiBuyPromotion()
        {
            var items = new object[]
            {
                new { Name = "Apples", Price = 4, Promotion = new { Type = "MULTI-BUY", DiscountPrice = 5, DiscountQuantity = 2 } }
            };
            var itemsString = JsonConvert.SerializeObject(items);
            var receipt = ReceiptDeserializer.Create(itemsString, "Apples", "Apples");
            var value = receipt.GetItems();
            Assert.AreEqual(5, value[0].Price);
        }

    }
}
