namespace BarmenShop.Entites
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public int BacketId { get; set; }
        public Basket Backet { get; set; }
    }
}
