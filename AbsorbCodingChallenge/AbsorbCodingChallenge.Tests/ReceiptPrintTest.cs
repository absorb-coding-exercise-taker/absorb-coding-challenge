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
        public void ItPrintsAnEmptyReceiptTotal()
        {
            var receipt = new Receipt();
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("Total: $0", receiptPrinter.PrintTotal());
        }
    }
}
