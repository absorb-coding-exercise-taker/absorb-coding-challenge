using System;

namespace AbsorbCodingChallenge.Promotions
{
    public class BuyOneGetOnePercentOff : IPromotion
    {
        public decimal DiscountPercent { get; set; }
        public decimal CalculatePrice(int quantity, decimal regularPrice)
        {
            var discountItems = Math.Floor(quantity / (decimal)2);
            var regularPricedItems = quantity - discountItems;
            var regularPricedTotal = (regularPricedItems * regularPrice);
            var discountPrice = regularPrice - (regularPrice/100*DiscountPercent);
            var discountTotal = discountItems*discountPrice;
            return regularPricedTotal + discountTotal;
        }
    }
}