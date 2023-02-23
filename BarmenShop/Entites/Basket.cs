namespace BarmenShop.Entites
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<BasketItem>? BacketItems { get; set; }
        public bool IsOrdered { get; set; }
    }
}
