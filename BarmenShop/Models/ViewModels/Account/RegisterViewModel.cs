using System.ComponentModel.DataAnnotations;

namespace BarmenShop.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Login")]
/*        [Remote("CheckLogin", "RegisterValidator", ErrorMessage = "Данный Login уже зарегистрирован")]*/
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
