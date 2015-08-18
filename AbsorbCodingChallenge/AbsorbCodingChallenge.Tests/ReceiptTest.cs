using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbsorbCodingChallenge.Tests
{
    [TestClass]
    public class ReceiptTest
    {
        [TestMethod]
        public void ItPrintsAnEmptyReceiptItems()
        {
            var receipt = new Receipt();
            var value = receipt.PrintItems();
            Assert.AreEqual("No items found", value);
        }

        [TestMethod]
        public void ItPrintsAnEmptyReceiptTotal()
        {
            var receipt = new Receipt();
            var value = receipt.PrintTotal();
            Assert.AreEqual("Total: $0", value);
        }

        [TestMethod]
        public void ItPrintsAReceiptWith1Item()
        {
            var receipt = new Receipt()
            {
                Items = new List<Item>()
                {
                    new Item {Name = "Apple"}
                }
            };
            var value = receipt.PrintItems().Trim();
            Assert.AreEqual("Apple x1", value);
        }
    }
}
