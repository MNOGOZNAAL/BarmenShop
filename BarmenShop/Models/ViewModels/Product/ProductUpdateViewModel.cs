using System.ComponentModel.DataAnnotations;

namespace BarmenShop.Models.ViewModels.Product
{
    public class ProductUpdateViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
