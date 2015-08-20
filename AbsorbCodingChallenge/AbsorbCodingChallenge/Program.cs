using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AbsorbCodingChallenge.Promotions;
using Newtonsoft.Json;

namespace AbsorbCodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the checkout application.");
            var store = new Store();

            while (true)
            {
                Console.WriteLine("Before I process the items in your cart, please specify a file which contains your prices (leave blank for sampleItems.json)");
                var fileName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = "../../sampleItems.json";
                }

                var content = System.IO.File.ReadAllText(fileName);
                var itemPrices = ItemPricesDeserializer.Create(content);
                store.AddItemPrices(itemPrices.ToArray());
                var scannedItems = ScannedItemsDeserializer.Create(args);
                var receipt = store.Checkout(scannedItems.ToArray());
                var printer = new ReceiptPrinter(receipt);

                Console.WriteLine(printer.Print());
                Console.ReadKey();
            }
        }
    }
}
