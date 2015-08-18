namespace AbsorbCodingChallenge.Promotions
{
    public class MultiBuy
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal CalculatePrice(int quantity, decimal regularPrice)
        {
            if (quantity < Quantity)
            {
                return quantity * regularPrice;
            }
            if (quantity > Quantity)
            {
                return Price + ((quantity % Quantity) * regularPrice);
            }
            return Price;
        }
    }
}