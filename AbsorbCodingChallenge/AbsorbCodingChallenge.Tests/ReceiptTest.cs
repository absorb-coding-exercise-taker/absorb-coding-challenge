using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbsorbCodingChallenge.Tests
{
    [TestClass]
    public class ReceiptTest
    {
        [TestMethod]
        public void ItCalculatesAnEmptyReceiptItemsList()
        {
            var receipt = new Receipt();
            var value = receipt.GetItems();
            Assert.AreEqual(0, value.Count);
        }

        [TestMethod]
        public void ItCalculatesAnEmptyReceiptTotal()
        {
            var receipt = new Receipt();
            var value = receipt.GetTotal();
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void ItGetsAReceiptWith1Item()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScanedItem>()
                {
                    new ScanedItem {Name = "Apple"}
                }
            };
            var value = receipt.GetItems();
            Assert.AreEqual(1, value.Count);
            Assert.AreEqual("Apple", value[0].Name);
            Assert.AreEqual(1, value[0].Quantity);
        }
        
    }
}
