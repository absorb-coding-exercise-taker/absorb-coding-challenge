using System;
using System.Runtime.Remoting.Messaging;

namespace AbsorbCodingChallenge.Promotions
{
    public class MultiBuy : IPromotion
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public CalculatePriceResult CalculatePrice(int quantity, decimal regularPrice)
        {
            var remainder = quantity % Quantity;

            var discountedItems = Math.Floor(quantity / (decimal)Quantity);
            var discountedPricedItems = discountedItems * Price;

            return new CalculatePriceResult(discountedPricedItems, remainder);
        }
    }
}