using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsorbCodingChallenge
{
    public class Store
    {
        private IList<ItemPrice> ItemPrices { get; set; }
        public Store()
        {
            ItemPrices = new List<ItemPrice>();
        }

        public void AddItemPrices(params ItemPrice[] prices)
        {
            foreach (var price in prices)
            {
                ItemPrices.Add(price);
            }
        } 

        public Receipt Checkout(params ScannedItem[] scannedItems)
        {
            var receipt = new Receipt()
            {
                ItemPrices = ItemPrices,
                ScannedItems = scannedItems,
            };
            return receipt;
        }
    }
}
