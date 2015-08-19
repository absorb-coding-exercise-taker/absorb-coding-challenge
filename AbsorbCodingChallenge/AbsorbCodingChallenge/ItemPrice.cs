using System.Runtime.InteropServices;

namespace AbsorbCodingChallenge
{
    public class ItemPrice
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Promotions.IPromotion Promotion { get;set; }

        public decimal CalculatePrice(int quantity)
        {
            decimal price = 0;
            if (Promotion != null)
            {
                var result = Promotion.CalculatePrice(quantity, Price);
                price = result.DiscountPrice;
                quantity = result.RegularPricedQuantity;
            }

            price = Price * quantity;

            return price;
        }
    }
}