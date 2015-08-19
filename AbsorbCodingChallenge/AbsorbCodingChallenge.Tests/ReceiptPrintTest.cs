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
        public void ItCalculatesAnEmptyReceiptItemsList()
        {
            var receipt = new Receipt();
            var receiptPrinter = new ReceiptPrinter(receipt);
            Assert.AreEqual("", receiptPrinter.Print());
        }
    }
}
