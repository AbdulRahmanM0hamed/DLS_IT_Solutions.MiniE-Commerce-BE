using Microsoft.AspNetCore.Identity;
using MiniE_Commerce.BLL.Helper.Global;
using MiniE_Commerce.DAL.Entities.Enums;
using MiniE_Commerce.DAL.Entities.User;

namespace MiniE_Commerce.BLL.Helper.SeedingData
{
    public class RoleSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleSeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedRolesAndAdminAsync()
        {
            string[] roles = { Roles.Admin, Roles.User };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }

            string adminEmail = "Admin@Admin.com";          
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    FirstName = "new",
                    LastName = "Admin",
                    UserName = "new-Admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "01234567891",
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male
                };

                var result = await _userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }
         
            string userEmail = "user@user.com";
            var regularUser = await _userManager.FindByEmailAsync(userEmail);
            if (regularUser == null)
            {
                regularUser = new ApplicationUser
                {
                    FirstName = "new",
                    LastName = "User",
                    UserName = "new-user",
                    Email = userEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "01012345678",
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Female
                };

                var result = await _userManager.CreateAsync(regularUser, "User@123");
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(regularUser, Roles.User);
            }
        }
    }
}
