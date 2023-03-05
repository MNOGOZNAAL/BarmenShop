namespace BarmenShop.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public virtual Basket Basket { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public decimal TotalSum { get; set; }
        public string Address { get; set; }
        public string Contasts { get; set; }
        public int Payment { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
