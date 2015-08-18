namespace AbsorbCodingChallenge.Promotions
{
    public class MultiBuy
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal CalculatePrice(int quantity)
        {
            return Price;
        }
    }
}