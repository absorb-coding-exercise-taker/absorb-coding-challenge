using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbsorbCodingChallenge.Tests
{
    [TestClass]
    public class ReceiptTest
    {
        [TestMethod]
        public void ItPrintsAnEmptyReceipt()
        {
            var receipt = new Receipt();
            var value = receipt.Print();
            Assert.AreEqual("No items found", value);
        }
    }
}
