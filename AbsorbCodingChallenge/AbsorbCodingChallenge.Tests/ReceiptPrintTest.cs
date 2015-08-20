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
            Assert.AreEqual("No items found", receiptPrinter.PrintItems().Trim());
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
            Assert.AreEqual("Apple x1\t$1.00", receiptPrinter.PrintItems().Trim());
        }

        [TestMethod]
        public void ItPrintsABasicReceiptItemWithDecimals()
        {
            var receipt = new Receipt
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice { Name = "Apple", Price = 0.75M}
                },
                ScannedItems = new List<ScannedItem>
                {
                    new ScannedItem() { Name="Apple" }
                }
            };
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Apple x1\t$0.75", receiptPrinter.PrintItems().Trim());
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
            Assert.AreEqual("Apple x2\t$2.00", receiptPrinter.PrintItems().Trim());
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
            Assert.AreEqual("Apple x2\t$10.00\r\nBanana x1\t$3.00", receiptPrinter.PrintItems().Trim());
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
            Assert.AreEqual("Apple x2\t$5.00", receiptPrinter.PrintItems().Trim());
        }

        [TestMethod]
        public void ItPrintsDiscountedPricesAwkwardDecimals()
        {
            var receipt = new Receipt
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice { Name = "Apple", Price = 0.3M, Promotion = new BuyOneGetOnePercentOff(10)},
                },
                ScannedItems = new List<ScannedItem>
                {
                    new ScannedItem() { Name="Apple" },
                    new ScannedItem() { Name="Apple" }
                }
            };
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Apple x2\t$0.57", receiptPrinter.PrintItems().Trim());
        }

        [TestMethod]
        public void ItPrintsAnEmptyReceiptTotal()
        {
            var receipt = new Receipt();
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Total: $0.00", receiptPrinter.PrintTotal());
        }

        [TestMethod]
        public void ItPrintsABasicReceiptTotal()
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
            Assert.AreEqual("Total: $1.00", receiptPrinter.PrintTotal());
        }

        [TestMethod]
        public void ItPrintsADiscountReceiptTotal()
        {
            var receipt = new Receipt
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice { Name = "Apple", Price = 0.5M, Promotion = new BuyOneGetOneFree()}
                },
                ScannedItems = new List<ScannedItem>
                {
                    new ScannedItem() { Name="Apple" },
                    new ScannedItem() { Name="Apple" }
                }
            };
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Total: $0.50", receiptPrinter.PrintTotal());
        }

        [TestMethod]
        public void ItPrintsAnEmptyReceipt()
        {
            var receipt = new Receipt();
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("No items found\r\nTotal: $0.00", receiptPrinter.Print());
        }

        [TestMethod]
        public void ItPrintsABasicReceipt()
        {
            var receipt = new Receipt
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice { Name = "Apple", Price = 0.5M, Promotion = new BuyOneGetOneFree()},
                    new ItemPrice { Name = "Banana", Price = 2.45M},
                },
                ScannedItems = new List<ScannedItem>
                {
                    new ScannedItem() { Name="Apple" },
                    new ScannedItem() { Name="Apple" },
                    new ScannedItem() { Name="Banana" }
                }
            };
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Apple x2\t$0.50\r\nBanana x1\t$2.45\r\nTotal: $2.95", receiptPrinter.Print());
        }
    }
}
