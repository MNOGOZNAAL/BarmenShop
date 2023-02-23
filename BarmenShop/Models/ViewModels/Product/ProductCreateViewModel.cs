using System.ComponentModel.DataAnnotations;

namespace BarmenShop.Models.ViewModels.Product
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Изображение")]
        public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
    }
}
