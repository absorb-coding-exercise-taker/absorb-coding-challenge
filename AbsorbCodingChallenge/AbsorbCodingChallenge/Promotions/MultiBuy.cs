using System;
using System.Runtime.Remoting.Messaging;

namespace AbsorbCodingChallenge.Promotions
{
    public class MultiBuy : IPromotion
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal CalculatePrice(int quantity, decimal regularPrice)
        {
            var remainder = quantity % Quantity;
            var regularPricedItems = remainder * regularPrice;

            var discountedItems = Math.Floor(quantity / (decimal)Quantity);
            var discountedPricedItems = discountedItems * Price;

            return regularPricedItems + discountedPricedItems;
        }
    }
    public class BuyOneGetOneFree : IPromotion
    {
        public decimal CalculatePrice(int quantity, decimal regularPrice)
        {
            return quantity * regularPrice;
        }
    }
}