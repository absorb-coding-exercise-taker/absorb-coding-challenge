using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsorbCodingChallenge
{
    public class Receipt
    {

        public IEnumerable<Item> Items { get; set; }
        public Receipt()
        {
            Items = new List<Item>();
        }

        public string PrintItems()
        {
            if (!Items.Any())
            {
                return "No items found";
            }

            var receipt = new StringBuilder();
            foreach (var item in Items)
            {
                receipt.AppendLine($"{item.Name} x1");
            }
            return receipt.ToString();
        }

        public string PrintTotal()
        {
            return "Total: $0";
        }

        public string Print()
        {
            return PrintItems() + "\r\n" + PrintTotal();
        }
    }

    public class Item
    {
        public string Name { get; set; }
    }
}
