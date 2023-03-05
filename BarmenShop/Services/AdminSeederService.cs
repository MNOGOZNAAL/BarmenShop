using BarmenShop.Entites;
using Microsoft.AspNetCore.Identity;

namespace BarmenShop.Services
{
    public class AdminSeederService
    {
        public static async Task SeedAdminUser(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            string adminLogin = "admin";
            string adminPassword = "adminpass";
            string adminEmail = "admin@email.kz";
            var roles = new[] { "admin", "user" };

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                {
                    await roleManager.CreateAsync(new Role { Name = role });
                }
            }
            if (await userManager.FindByNameAsync(adminLogin) is null)
            {
                User admin = new User
                {
                    UserName = adminLogin,
                    RegDate = DateTime.Now,
                    Email = adminEmail,
                };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
