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

        public IEnumerable<ScannedItem> ScannedItems { get; set; }
        public Receipt()
        {
            ScannedItems = new List<ScannedItem>();
        }

        public IList<ReceiptItem> GetItems()
        {
            var items = new List<ReceiptItem>();

            var scannedItemsGrouped = GetGroupedScannedItems();
            items.AddRange(scannedItemsGrouped.Select(i => new ReceiptItem
            {
                Name = i.Key,
                Quantity = i.Count()
            }));

            return items;
        }

        private IEnumerable<IGrouping<string, ScannedItem>> GetGroupedScannedItems()
        {
            return ScannedItems.GroupBy(g => g.Name);
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
