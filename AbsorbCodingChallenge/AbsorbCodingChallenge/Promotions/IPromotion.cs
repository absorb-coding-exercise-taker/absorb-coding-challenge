namespace AbsorbCodingChallenge.Promotions
{
    public interface IPromotion
    {
        CalculatePriceResult CalculatePrice(int quantity, decimal regularPrice);
    }

    public class CalculatePriceResult
    {
        public CalculatePriceResult(decimal discountPrice, int regularPricedQuantity)
        {
            DiscountPrice = discountPrice;
            RegularPricedQuantity = regularPricedQuantity;
        }

        public decimal DiscountPrice { get; set; }
        public int RegularPricedQuantity { get; set; }
    }
}