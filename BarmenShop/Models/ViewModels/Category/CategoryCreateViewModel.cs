using System.ComponentModel.DataAnnotations;

namespace BarmenShop.Models.ViewModels.Category
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}
