using BarmenShop.Models.Responces;
using BarmenShop.Models.ViewModels.Account;

namespace BarmenShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticationResult> Register(RegisterViewModel model);
        Task<AuthenticationResult> LogIn(LoginViewModel model);
        Task LogOutAsync();
    }
}
