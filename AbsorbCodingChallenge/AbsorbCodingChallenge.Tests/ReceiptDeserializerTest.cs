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
        
    }
}
