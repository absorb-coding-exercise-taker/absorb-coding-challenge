namespace AbsorbCodingChallenge
{
    public class ItemPrice
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Promotions.MultiBuy Promotion { get;set; }
    }
}