using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WhatsForDinner.Models{

    public static class IdentitySeedData{

        private const string user = "user";
        private const string password = "Password_1234";
        private const string userRole = "User";

        public static async void EnsurePopulated(IApplicationBuilder app){
            AppIdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            if(context.Database.GetPendingMigrations().Any()){
                context.Database.Migrate();
            }
            UserManager<IdentityUser> userManager = 
                app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            var roleManager = app.ApplicationServices.CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();
            
            if (await roleManager.FindByNameAsync(userRole) == null){
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }

            IdentityUser? _user = await userManager.FindByNameAsync(user);
            if (_user == null){
                _user = new IdentityUser(user)
                {
                    Email = "user@test.com"
                };
                await userManager.CreateAsync(_user, password);
                await userManager.AddToRoleAsync(_user, userRole);
            }
        }
        


    }
    
}