namespace AbsorbCodingChallenge
{
    public class ItemPrice
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Promotions.MultiBuy Promotion { get;set; }

        public decimal CalculatePrice(int quantity)
        {
            decimal price;
            if (Promotion != null)
            {
                price = Promotion.CalculatePrice(quantity);
            }
            else
            {
                price = Price * quantity;
            }

            return price;
        }
    }
}