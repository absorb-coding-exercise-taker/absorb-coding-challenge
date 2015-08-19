using System;
using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;
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
        public void ItPrintsABasicReceiptItem()
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
        public void ItPrintsABasicReceiptMultipleItems()
        {
            var receipt = new Receipt
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice { Name = "Apple", Price = 1}
                },
                ScannedItems = new List<ScannedItem>
                {
                    new ScannedItem() { Name="Apple" },
                    new ScannedItem() { Name="Apple" }
                }
            };
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Apple x2\t$2", receiptPrinter.PrintItems().Trim());
        }

        [TestMethod]
        public void ItPrintsABasicReceiptMultipleItemsOfDifferentTypes()
        {
            var receipt = new Receipt
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice { Name = "Apple", Price = 5},
                    new ItemPrice { Name = "Banana", Price = 3}
                },
                ScannedItems = new List<ScannedItem>
                {
                    new ScannedItem() { Name="Apple" },
                    new ScannedItem() { Name="Banana" },
                    new ScannedItem() { Name="Apple" }
                }
            };
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Apple x2\t$10\r\nBanana x1\t$3", receiptPrinter.PrintItems().Trim());
        }

        [TestMethod]
        public void ItPrintsDiscountedPrices()
        {
            var receipt = new Receipt
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice { Name = "Apple", Price = 5, Promotion = new BuyOneGetOneFree()},
                },
                ScannedItems = new List<ScannedItem>
                {
                    new ScannedItem() { Name="Apple" },
                    new ScannedItem() { Name="Apple" }
                }
            };
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Apple x2\t$5", receiptPrinter.PrintItems().Trim());
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
