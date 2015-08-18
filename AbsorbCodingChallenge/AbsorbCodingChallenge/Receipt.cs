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
        public IEnumerable<ItemPrice> ItemPrices { get; set; }
        public Receipt()
        {
            ScannedItems = new List<ScannedItem>();
            ItemPrices = new List<ItemPrice>();
        }

        public IList<ReceiptItem> GetItems()
        {
            var items = new List<ReceiptItem>();

            var scannedItemsGrouped = GetGroupedScannedItems();
            items.AddRange(scannedItemsGrouped.Select(i => new ReceiptItem
            {
                Name = i.Key,
                Quantity = i.Count(),
                Price = GetPriceForItem(i.Key, i.Count())
            }));

            return items;
        }

        private decimal? GetPriceForItem(string name, int quantity)
        {
            var itemPrice = ItemPrices.FirstOrDefault(p => p.Name == name);
            if (itemPrice == null)
            {
                return null;
            }

            decimal? price;
            if (itemPrice.Promotion != null)
            {
                price = itemPrice.Promotion.CalculatePrice(quantity);
            }
            else
            {
                price = itemPrice.Price * quantity;
            }

            return price;
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
