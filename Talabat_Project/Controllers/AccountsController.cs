using Core.Entites.Identity;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;
using Talabat_Project.DTOs;
using Talabat_Project.Errors;

namespace Talabat_Project.Controllers
{
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService Token;

        public AccountsController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager,ITokenService token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Token = token;
        }
        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto Model)
        {
            var User = new AppUser()
            {
                DisplayName = Model.DisplayName,
                Email = Model.Email,
                UserName= Model.Email.Split('@')[0],
                PhoneNumber = Model.PhoneNumber
            };
            var Result = await _userManager.CreateAsync(User, Model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            var ReturnedUser = new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await Token.CreateTokenAsync(User, _userManager)
            };
            return Ok(ReturnedUser);
        }

        // LogIn
        [HttpPost("LogIn")]
        public async Task<ActionResult<UserDto>>LogIn(LogInDto Model)
        {
          var User=await _userManager.FindByEmailAsync(Model.Email);
          if (User is null) return Unauthorized(new ApiResponse(401));
          var Result = await _signInManager.CheckPasswordSignInAsync(User, Model.Password, false);
          if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));
            var ReturnedUser = new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await Token.CreateTokenAsync(User,_userManager)
            };
            return Ok(ReturnedUser);
        }
        // Get Currnet User
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>>GetCurrentUser(LogInDto Model)
        {
            var Email= User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            var ReturnedUser = new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await Token.CreateTokenAsync(user, _userManager)
            };
            return Ok(ReturnedUser);
        }
        
    }
}
