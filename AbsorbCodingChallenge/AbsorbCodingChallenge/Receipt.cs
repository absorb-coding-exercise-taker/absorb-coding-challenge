using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsorbCodingChallenge
{
    public class Receipt
    {

        public IEnumerable<ScanedItem> ScannedItems { get; set; }
        public Receipt()
        {
            ScannedItems = new List<ScanedItem>();
        }

        public IList<ReceiptItem> GetItems()
        {
            var items = new List<ReceiptItem>();

            items.AddRange(ScannedItems.Select(item => new ReceiptItem {Name = item.Name, Quantity = 1}));

            return items;
        }

        public decimal GetTotal()
        {
            return 0;
        }

        public string Print()
        {
            return GetItems() + "\r\n" + GetTotal();
        }
    }
}
