using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbsorbCodingChallenge.Promotions;
using Newtonsoft.Json;

namespace AbsorbCodingChallenge
{
    public static class ReceiptDeserializer
    {
        public static Receipt Create(string prices, params string[] items)
        {
            var fileData = JsonConvert.DeserializeObject<dynamic[]>(prices);

            var itemPrices = fileData.Select(item => CreateItemPrice(item)).Cast<ItemPrice>().ToList();

            return Create(itemPrices, items);
        }

        private static Receipt Create(IList<ItemPrice> prices, params string[] items)
        {
            var receipt = new Receipt()
            {
                ItemPrices = prices,
                ScannedItems = CreateScannedItems(items)
            };
            return receipt;
        }

        private static IList<ScannedItem> CreateScannedItems(string[] items)
        {
            return items.Select(c => new ScannedItem
            {
                Name = c
            }).ToList();
        }

        private static ItemPrice CreateItemPrice(dynamic item)
        {
            var itemPrice = new ItemPrice
            {
                Name = item.Name,
                Price = item.Price
            };
            if (item.Promotion != null)
            {
                switch ((string)item.Promotion.Type)
                {
                    case "BOGO":
                        itemPrice.Promotion = new BuyOneGetOneFree();
                        break;
                    case "BOGO-PERCENT":
                        itemPrice.Promotion = new BuyOneGetOnePercentOff((decimal)item.Promotion.DiscountPercent);
                        break;
                    case "MULTI-BUY":
                        itemPrice.Promotion = new MultiBuy { Price = item.Promotion.DiscountPrice, Quantity = item.Promotion.DiscountQuantity };
                        break;
                }
            }
            return itemPrice;
        }
    }
}
