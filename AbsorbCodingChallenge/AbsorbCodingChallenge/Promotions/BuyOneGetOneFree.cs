using System;

namespace AbsorbCodingChallenge.Promotions
{
    public class BuyOneGetOneFree : IPromotion
    {
        public decimal CalculatePrice(int quantity, decimal regularPrice)
        {
            var freeItems = Math.Floor(quantity/(decimal)2);
            return (quantity - freeItems)*regularPrice;
        }
    }
}