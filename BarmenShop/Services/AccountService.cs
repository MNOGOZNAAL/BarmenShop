using BarmenShop.Entites;
using BarmenShop.Enums;
using BarmenShop.Models.Responces;
using BarmenShop.Models.ViewModels.Account;
using BarmenShop.Repositories.Interfaces;
using BarmenShop.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BarmenShop.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResult> Register(RegisterViewModel? model)
        {
            if (model is null) return new AuthenticationResult
            {
                StatusCodes = RegStatusCodes.Error,
                ErrorMessages = new List<string> { "Внутренняя ошибка" },
            };

            User user = new User
            {
                UserName = model.Login,
                RegDate = DateTime.Now,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                await _signInManager.SignInAsync(user, true);
                return new AuthenticationResult { StatusCodes = RegStatusCodes.Success };
            }

            var errors = result.Errors.Select(error => error.Description).ToList();

            return new AuthenticationResult
            {
                ErrorMessages = errors,
                StatusCodes = RegStatusCodes.Error
            };
        }

        public async Task<AuthenticationResult> LogIn(LoginViewModel model)
        {
            User user = await _userManager.FindByNameAsync(model.Login);

            if (user is not null)
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(
                    user,
                    model.Password,
                    model.RememberMe,
                    false
                );
                if (result.Succeeded)
                    return new AuthenticationResult { StatusCodes = RegStatusCodes.Success };
            }
            return new AuthenticationResult
            {
                StatusCodes = RegStatusCodes.Error,
                ErrorMessages = new List<string> { "Неправильный логин и (или) пароль" }
            };
        }

        public async Task LogOutAsync()
            => await _signInManager.SignOutAsync();
    }
}
