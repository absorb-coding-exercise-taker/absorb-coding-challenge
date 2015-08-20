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
            var fileData = JsonConvert.DeserializeObject<dynamic>(prices);

            var itemPrices = new List<ItemPrice>();
            foreach (var item in fileData)
            {
                var itemPrice = new ItemPrice
                {
                    Name = item.Name,
                    Price = item.Price
                };
                if (item.Promotion != null)
                {
                    switch ((string)item.Promotion)
                    {
                        case "BOGO":
                            itemPrice.Promotion = new BuyOneGetOneFree();
                            break;
                        case "BOGO-PERCENT":
                            itemPrice.Promotion = new BuyOneGetOnePercentOff((decimal)item.DiscountPercent);
                            break;
                        case "MULTI-BUY":
                            itemPrice.Promotion = new MultiBuy { Price = item.DiscountPrice, Quantity = item.DiscountQuantity };
                            break;
                    }
                }
                itemPrices.Add(itemPrice);
            }
            var receipt = new Receipt()
            {
                ItemPrices = itemPrices,
                ScannedItems = items.Select(c => new ScannedItem
                {
                    Name = c
                })
            };
            return receipt;
        }
    }
}
