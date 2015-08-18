using System;

namespace AbsorbCodingChallenge.Promotions
{
    public class BuyOneGetOnePercentOff : IPromotion
    {
        public decimal DiscountPercent { get; set; }
        public CalculatePriceResult CalculatePrice(int quantity, decimal regularPrice)
        {
            var discountItems = (int)Math.Floor(quantity / (decimal)2);
            var regularPricedItems = quantity - discountItems;
            var discountPrice = regularPrice - (regularPrice/100*DiscountPercent);
            var discountTotal = discountItems*discountPrice;
            return new CalculatePriceResult(discountTotal, regularPricedItems);
        }
    }
}