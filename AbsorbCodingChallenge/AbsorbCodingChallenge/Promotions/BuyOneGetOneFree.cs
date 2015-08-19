using System;

namespace AbsorbCodingChallenge.Promotions
{
    public class BuyOneGetOneFree : IPromotion
    {
        private readonly BuyOneGetOnePercentOff promo;
        public BuyOneGetOneFree()
        {
            promo = new BuyOneGetOnePercentOff(100);
        }
        public CalculatePriceResult CalculatePrice(int quantity, decimal regularPrice)
        {
            return promo.CalculatePrice(quantity, regularPrice);
        }
    }
}