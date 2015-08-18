using System;
using System.Collections.Generic;
using System.Linq;
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
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem {Name = "Apple"}
                }
            };
            var value = receipt.GetItems();
            Assert.AreEqual(1, value.Count);
            Assert.AreEqual("Apple", value[0].Name);
            Assert.AreEqual(1, value[0].Quantity);
        }

        [TestMethod]
        public void ItGetsAReceiptWith2Items()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem {Name = "Apple"},
                    new ScannedItem {Name = "Banana"}
                }
            };
            var value = receipt.GetItems();
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual("Apple", value[0].Name);
            Assert.AreEqual(1, value[0].Quantity);
            Assert.AreEqual("Banana", value[1].Name);
            Assert.AreEqual(1, value[1].Quantity);
        }

        [TestMethod]
        public void ItGetsAReceiptWith1ItemTwice()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem {Name = "Apple"},
                    new ScannedItem {Name = "Apple"}
                }
            };
            var value = receipt.GetItems();
            Assert.AreEqual(1, value.Count);
            Assert.AreEqual("Apple", value[0].Name);
            Assert.AreEqual(2, value[0].Quantity);
        }

        [TestMethod]
        public void ItGetsAReceiptWith1ItemTwiceAnd1ItemOnce()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem {Name = "Apple"},
                    new ScannedItem {Name = "Apple"},
                    new ScannedItem {Name = "Banana"}
                }
            };
            var value = receipt.GetItems();
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual("Apple", value[0].Name);
            Assert.AreEqual(2, value[0].Quantity);
            Assert.AreEqual("Banana", value[1].Name);
            Assert.AreEqual(1, value[1].Quantity);
        }

        [TestMethod]
        public void ItGetsAReceiptWith1ItemTwiceAnd1ItemOnceOutOfOrder()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem {Name = "Apple"},
                    new ScannedItem {Name = "Banana"},
                    new ScannedItem {Name = "Apple"},
                }
            };
            var value = receipt.GetItems();
            Assert.AreEqual(2, value.Count);
            Assert.AreEqual("Apple", value[0].Name);
            Assert.AreEqual(2, value[0].Quantity);
            Assert.AreEqual("Banana", value[1].Name);
            Assert.AreEqual(1, value[1].Quantity);
        }

        [TestMethod]
        public void ItGetsAReceiptWithNoPrice()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                },
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual("Apple", value.Name);
            Assert.AreEqual(null, value.Price);
        }

        [TestMethod]
        public void ItGetsAReceiptWith1RegularPricedItem()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1}
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual("Apple", value.Name);
            Assert.AreEqual(1, value.Price);
        }

    }
}
