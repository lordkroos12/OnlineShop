using Microsoft.AspNetCore.Identity;
using OnlineShopUI.constants;

namespace OnlineShopUI.Data
{
    public class DbSeed
    {
        public static async Task SeedRoles(IServiceProvider service)
        { 
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //create account with admin role

            var admin = new IdentityUser
            {

                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,

            };
            var userExist = await userManager.FindByEmailAsync("admin@gmail.com");
            if (userExist is null)
            {
                await userManager.CreateAsync(admin, "@Admin123");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}
