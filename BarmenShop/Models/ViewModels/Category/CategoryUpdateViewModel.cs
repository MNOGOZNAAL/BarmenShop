using System.ComponentModel.DataAnnotations;

namespace BarmenShop.Models.ViewModels.Category
{
    public class CategoryUpdateViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
