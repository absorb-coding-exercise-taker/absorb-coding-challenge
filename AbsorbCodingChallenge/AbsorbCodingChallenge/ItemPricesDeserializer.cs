using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;
using Newtonsoft.Json;

namespace AbsorbCodingChallenge
{
    public static class ItemPricesDeserializer
    {
        public static IList<ItemPrice> Create(string prices)
        {
            var fileData = JsonConvert.DeserializeObject<dynamic[]>(prices);

            var itemPrices = fileData.Select(item => CreateItemPrice(item)).Cast<ItemPrice>().ToList();
            return itemPrices;
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
                        itemPrice.Promotion = new MultiBuy { Price = item.Promotion.Price, Quantity = item.Promotion.Quantity };
                        break;
                }
            }
            return itemPrice;
        }
    }
}
