using Core.Entites.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat_Project.DTOs;
using Talabat_Project.Errors;

namespace Talabat_Project.Controllers
{
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto Model)
        {
            var user = new AppUser()
            {
                Email = Model.Email,
                DisplayName = Model.DisplayName,
                UserName = Model.Email.Split('@')[0],
                PhoneNumber = Model.PhoneNumber,
            };
            var Result= await _userManager.CreateAsync(user, Model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
          
            var userDto = new UserDto()
            {
                Email = Model.Email,
                DisplayName = Model.DisplayName,
                Token = "Token"
            };
            return Ok(userDto);
        }

        // LogIn
        [HttpPost("LogIn")]
        public async Task<ActionResult<UserDto>>LogIn(LogInDto Model)
        {
           var User= await _userManager.FindByEmailAsync(Model.Email);
            if (User == null) return Unauthorized(new ApiResponse(401));
            var Result= await _signInManager.CheckPasswordSignInAsync(User, Model.Password, false);
            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));
            var userDto = new UserDto()
            {
                Email = User.Email,
                DisplayName = User.DisplayName,
                Token = "Token"
            };
            return Ok(userDto);
        }
    }
}
