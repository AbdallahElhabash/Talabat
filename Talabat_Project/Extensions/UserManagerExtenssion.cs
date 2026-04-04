using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Talabat_Project.Extensions
{
    public static class UserManagerExtenssion
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> UserManager, ClaimsPrincipal user)
        {
            var email=user.FindFirstValue(ClaimTypes.Email);
            var _user = await UserManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email);
            return _user;
        }
    }
}
