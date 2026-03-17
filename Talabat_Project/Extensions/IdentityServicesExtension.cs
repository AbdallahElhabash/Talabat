using Core.Entites.Identity;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Repository.Identity;
using Services;
using System.Runtime.CompilerServices;
using System.Text;

namespace Talabat_Project.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration configuration)
        {
          Services.AddScoped(typeof(ITokenService), typeof(TokenService));
          Services.AddIdentity<AppUser, IdentityRole>()
                  .AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(Options =>
                    {
                        Options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer=true,
                            ValidIssuer = configuration["JWT:Issuer"],
                            ValidateAudience=true,
                            ValidAudience = configuration["JWT:Audience"],
                            ValidateLifetime=true,
                            ValidateIssuerSigningKey=true,
                            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                        };
                    });

            return Services;
        }
    }
}
