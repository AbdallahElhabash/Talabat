using AutoMapper;
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
using Talabat_Project.Extensions;

namespace Talabat_Project.Controllers
{
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService Token;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager,ITokenService token,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Token = token;
            _mapper = mapper;
        }
        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto Model)
        {
            if (CheckDuplicateEmail(Model.Email).Result.Value)
                return BadRequest(new ApiResponse(400, "This Email is Aready in Use"));

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

        // Get Currnt User Address
        [Authorize]
        [HttpGet("CurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var MappedAddress = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(MappedAddress);
        }

        [Authorize]
        [HttpPut("Address")]
         public async Task<ActionResult<AddressDto>>UpdateAddress(AddressDto UpdatedAddress)
        {
            var user=await _userManager.FindUserWithAddressAsync(User);
            if (user is null) return Unauthorized(new ApiResponse(401));
            var address = _mapper.Map<AddressDto, Address>(UpdatedAddress);
            address.Id=user.Address.Id;
            user.Address = address;
            var Result=await _userManager.UpdateAsync(user);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(UpdatedAddress);
        }

        // Validate Duplicate Email at Register EndPoint
        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>>CheckDuplicateEmail(string email)
        {
            var user=await _userManager.FindByEmailAsync(email);
            bool result=user is null ? false : true;
            return Ok(result);
        }
    }
}
