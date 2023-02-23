namespace BarmenShop.Entites
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserCreatorId { get; set; }
        public User UserCreator { get; set; }
    }
}
