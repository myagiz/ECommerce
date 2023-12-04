using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Identity
{
    public class DefaultUserInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultUserInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeDefaultUser()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new ApplicationRole
                    {
                        Name = "Admin"
                    };
                    await roleManager.CreateAsync(role);
                }

                var defaultUser = await userManager.FindByEmailAsync("admin@admin.com");
                if (defaultUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admin@admin.com",
                        Email = "admin@admin.com",
                        FirstName = "Admin FirstName",
                        LastName = "Admin LastName",
                        IsActive = true
                    };

                    var result = await userManager.CreateAsync(user, "123456");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                    }
                }
            }
        }
    }

}
