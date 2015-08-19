using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbsorbCodingChallenge.Tests
{
    [TestClass]
    public class ReceiptPrintTest
    {
        [TestMethod]
        public void ItPrintsAnEmptyReceiptItems()
        {
            var receipt = new Receipt();
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("No items found", receiptPrinter.PrintItems());
        }

        [TestMethod]
        public void ItPrintsABasicReceiptItems()
        {
            var receipt = new Receipt
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice { Name = "Apple", Price = 1}
                },
                ScannedItems = new List<ScannedItem>
                {
                    new ScannedItem() { Name="Apple" }
                }
            };
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Apple x1\t$1", receiptPrinter.PrintItems().Trim());
        }

        [TestMethod]
        public void ItPrintsAnEmptyReceiptTotal()
        {
            var receipt = new Receipt();
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Total: $0", receiptPrinter.PrintTotal());
        }
    }
}
