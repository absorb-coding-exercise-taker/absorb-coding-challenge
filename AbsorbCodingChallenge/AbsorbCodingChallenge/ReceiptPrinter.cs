﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsorbCodingChallenge
{
    public class ReceiptPrinter
    {
        private readonly Receipt _receipt;
        private IList<ReceiptItem> _items;
        private decimal _total;
        public ReceiptPrinter(Receipt receipt)
        {
            _receipt = receipt;
             _items = _receipt.GetItems();
            _total = _receipt.GetTotal();
        }

        public string PrintItems()
        {
            if (!_receipt.ScannedItems.Any())
            {
                return "No items found\r\n";
            }

            var items = new StringBuilder();
            foreach (var item in _receipt.GetItems())
            {
                items.AppendLine($"{item.Name} x{item.Quantity}\t${item.Price.ToString("0.00")}");
            }
            return items.ToString();
        }

        public string PrintTotal()
        {
            return $"Total: ${_total.ToString("0.00")}";
        }

        public string Print()
        {
            return PrintItems() + PrintTotal();
        }
    }
}
