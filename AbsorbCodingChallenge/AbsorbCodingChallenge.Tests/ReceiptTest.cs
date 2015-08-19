using System;
using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;
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
        public void ItThrowsAnExceptionWhenNoItemPriceDefined()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem {Name = "Apple"}
                }
            };
            try
            {
                receipt.GetItems();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Item price for Apple is not defined", e.Message);
            }
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
            Assert.AreEqual(1, value.Price);
        }

        [TestMethod]
        public void ItGetsAReceiptWith2RegularPricedItems()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1}
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(2, value.Price);
        }

        [TestMethod]
        public void ItGetsAReceiptWith2RegularPricedItemsAndOneOtherItem()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Banana" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1},
                    new ItemPrice() { Name = "Banana", Price = 3}
                }
            };
            var value = receipt.GetItems();
            Assert.AreEqual("Apple", value[0].Name);
            Assert.AreEqual(2, value[0].Price);
            Assert.AreEqual(2, value[0].Quantity);
            Assert.AreEqual("Banana", value[1].Name);
            Assert.AreEqual(3, value[1].Price);
            Assert.AreEqual(1, value[1].Quantity);
        }

        [TestMethod]
        public void ItGetsAReceiptWithMultiBuyPromo()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1, Promotion = new Promotions.MultiBuy { Quantity = 3, Price = 1 } }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(1, value.Price);
        }

        [TestMethod]
        public void ItGetsAReceiptWithMultiBuyPromoOverflow()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1, Promotion = new Promotions.MultiBuy { Quantity = 3, Price = 1 } }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(2, value.Price);
        }

        [TestMethod]
        public void ItGetsAReceiptWithMultiBuyPromoDoubleOverflow()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1, Promotion = new Promotions.MultiBuy { Quantity = 2, Price = 1 } }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(3, value.Price);
        }

        [TestMethod]
        public void ItGetsAReceiptWith2MultiBuyTypes()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Banana" },
                    new ScannedItem { Name = "Banana" },
                    new ScannedItem { Name = "Banana" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1, Promotion = new Promotions.MultiBuy { Quantity = 2, Price = 1 } },
                    new ItemPrice() { Name = "Banana", Price = 2, Promotion = new Promotions.MultiBuy { Quantity = 3, Price = 5 } }
                }
            };
            var apple = receipt.GetItems().First();
            var banana = receipt.GetItems().ElementAt(1);
            Assert.AreEqual(1, apple.Price);
            Assert.AreEqual(5, banana.Price);
        }

        [TestMethod]
        public void ItGetsReceiptBogoPromoUnderflow()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 2, Promotion = new Promotions.BuyOneGetOneFree() }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(2, value.Price);
        }

        [TestMethod]
        public void ItGetsReceiptBogoPromoMatch()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 2, Promotion = new Promotions.BuyOneGetOneFree() }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(2, value.Price);
        }

        [TestMethod]
        public void ItGetsReceiptWithBogoPromoOverflow()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 2, Promotion = new Promotions.BuyOneGetOneFree() }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(4, value.Price);
        }

        [TestMethod]
        public void ItGetsReceiptBogo50PromoUnderflow()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 2, Promotion = new Promotions.BuyOneGetOnePercentOff(50) }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(2, value.Price);
        }

        [TestMethod]
        public void ItGetsReceiptBogo50PromoMatch()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 2, Promotion = new Promotions.BuyOneGetOnePercentOff(50) }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(3, value.Price);
        }

        [TestMethod]
        public void ItGetsReceiptBogo50PromoOverflow()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 2, Promotion = new Promotions.BuyOneGetOnePercentOff(50) }
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(5, value.Price);
        }

        [TestMethod]
        public void ItIgnoresDuplicatePricesExceptTheLastOne()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1},
                    new ItemPrice() { Name = "Apple", Price = 2}
                }
            };
            var value = receipt.GetItems().First();
            Assert.AreEqual(2, value.Price);
        }

        [TestMethod]
        public void ItCalculatesATotalWith1Item()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1 }
                }
            };
            Assert.AreEqual(1, receipt.GetTotal());
        }

        [TestMethod]
        public void ItCalculatesATotalWith1ItemDecimal()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 0.75M }
                }
            };
            Assert.AreEqual(0.75M, receipt.GetTotal());
        }

        [TestMethod]
        public void ItCalculatesATotalWithDuplicateItem()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1 }
                }
            };
            Assert.AreEqual(2, receipt.GetTotal());
        }

        [TestMethod]
        public void ItCalculatesATotalWithDuplicateItemAndDiscounts()
        {
            var receipt = new Receipt()
            {
                ScannedItems = new List<ScannedItem>()
                {
                    new ScannedItem { Name = "Apple" },
                    new ScannedItem { Name = "Apple" },
                },
                ItemPrices = new List<ItemPrice>()
                {
                    new ItemPrice() { Name = "Apple", Price = 1, Promotion = new BuyOneGetOneFree()}
                }
            };
            Assert.AreEqual(1, receipt.GetTotal());
        }
    }
}
