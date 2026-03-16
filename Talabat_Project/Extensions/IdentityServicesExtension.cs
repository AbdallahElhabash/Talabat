using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Repository.Identity;
using System.Runtime.CompilerServices;

namespace Talabat_Project.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
          Services.AddIdentity<AppUser, IdentityRole>()
                  .AddEntityFrameworkStores<AppIdentityDbContext>();
          Services.AddAuthentication(); // User Manager , SignIn Manager , Role Manager

            return Services;
        }
    }
}
