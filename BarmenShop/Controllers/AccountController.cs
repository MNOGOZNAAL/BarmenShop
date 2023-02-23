using BarmenShop.Enums;
using BarmenShop.Models.ViewModels.Account;
using BarmenShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarmenShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.Register(model);
            if (result.StatusCodes == RegStatusCodes.Success)
                return RedirectToAction("Index", "Product");

            foreach (var error in result.ErrorMessages)
                ModelState.AddModelError(string.Empty, error);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.LogIn(model);
            if (result.StatusCodes == RegStatusCodes.Success)
            {
                return RedirectToAction("Index", "Product");
            }

            foreach (var error in result.ErrorMessages)
                ModelState.AddModelError(string.Empty, error);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
