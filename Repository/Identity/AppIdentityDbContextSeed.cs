using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Identity
{
      public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser>userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Abdallah Elhabash",
                    Email = "abdallahm.elhabash@gmail.com",
                    UserName = "Abdallah.Elhabash",
                    PhoneNumber = "01013395960"
                };
                await userManager.CreateAsync(user, "Ae@Password1");
            }
       
        }
    }
}
