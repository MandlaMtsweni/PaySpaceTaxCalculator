using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Data;

namespace TaxCalculator.Areas
{
    public class InitialUser
    {
        // This will create a user with an Admin role when the application runs for the first time.
        // It will also create the only two roles the users will have for this application, namely Admin and User role.

        public async static void Initialize(IServiceProvider serviceProvider)
        {
            var _dbContext = serviceProvider.GetRequiredService<TaxCalculatorDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            _dbContext.Database.EnsureCreated();

            if (!_dbContext.Users.Any())
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = "Admin",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = "admin@payspace.com"
                };

                // Create Admin user
                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        // Create Admin role and apply it to the Admin user
                        var adminRole = new IdentityRole("Admin");

                        var res = await roleManager.CreateAsync(adminRole);
                        if (res.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, "Admin");
                        }

                        // Create User role
                        var userRole = new IdentityRole("User");
                        await roleManager.CreateAsync(userRole);
                    }
                }
            }

        }
    }
}
