using System;
using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;
using Newtonsoft.Json;

namespace AbsorbCodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the checkout application. Before I process your shopping list, please specify a file which contains your prices (leave blank for sample)");
            var fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "../../sampleItems.json";
            }
            var content = System.IO.File.ReadAllText(fileName);
            var fileData = JsonConvert.DeserializeObject<dynamic>(content);

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
                    switch ((string) item.Promotion)
                    {
                        case "BOGO":
                            itemPrice.Promotion = new BuyOneGetOneFree();
                            break;
                        case "BOGO-PERCENT":
                            itemPrice.Promotion = new BuyOneGetOnePercentOff((decimal) item.DiscountPercent);
                            break;
                        case "MULTI-BUY":
                            itemPrice.Promotion = new MultiBuy{ Price = item.DiscountPrice, Quantity = item.DiscountQuantity};
                            break;
                    }
                }
                itemPrices.Add(itemPrice);
            }
            var receipt = new Receipt()
            {
                ItemPrices = itemPrices,
                ScannedItems = args.Select(c => new ScannedItem
                {
                    Name = c
                })
            };
            var printer = new ReceiptPrinter(receipt);

            Console.WriteLine(printer.Print());
            Console.ReadKey();
        }
    }
}
