using Core.Entites.Identity;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration Configuration;

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser User,UserManager<AppUser>userManager)
        {
           // PayLoads
           // 1. Private Claims
           var AuthClaims=new List<Claim>()
           {
               new Claim(ClaimTypes.GivenName,User.DisplayName),
               new Claim(ClaimTypes.Email,User.Email)
           };
            var UserRoles = await userManager.GetRolesAsync(User);
            foreach(var Role in UserRoles)
            {
              AuthClaims.Add(new Claim(ClaimTypes.Role,Role));
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));

            var Token = new JwtSecurityToken
                (
                issuer: Configuration["JWT:Issuer"],
                audience: Configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(Configuration["JWT:DurationInDays"])),
                claims:AuthClaims,
                signingCredentials:new SigningCredentials(Key,SecurityAlgorithms.HmacSha256Signature)
                ) ;
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
