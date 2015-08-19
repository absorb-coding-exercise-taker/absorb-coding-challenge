using System;
using System.Collections.Generic;
using System.Linq;
using AbsorbCodingChallenge.Promotions;

namespace AbsorbCodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var receipt = new Receipt()
            {
                ItemPrices = new List<ItemPrice>
                {
                    new ItemPrice {Name = "Apple", Price = 1},
                    new ItemPrice {Name = "Banana", Price = 0.75M, Promotion = new BuyOneGetOneFree()},
                    new ItemPrice {Name = "Orange", Price = 2.25M},
                },
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
