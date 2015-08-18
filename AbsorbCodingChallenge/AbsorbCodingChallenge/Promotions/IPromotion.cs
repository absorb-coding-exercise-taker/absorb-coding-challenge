namespace AbsorbCodingChallenge.Promotions
{
    public interface IPromotion
    {
        decimal CalculatePrice(int quantity, decimal regularPrice);
    }
}