using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ThutoConnectAPI.Data;
using ThutoConnectAPI.DTOs.Account;
using ThutoConnectAPI.Models;
using ThutoConnectAPI.Services;

namespace ThutoConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(JWTService jwtService,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            Context context,
            IConfiguration config)
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _userManager = userManager;
            
            
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefereshUserToken()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value);
            return CreateApplicationUserDto(user);
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) return Unauthorized("Invalid username or password");

            if (user.EmailConfirmed == false) return Unauthorized("Please confirm your email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid username or password");

            return CreateApplicationUserDto(user);



        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {

            if(await CheckEmailExistsAsync(model.Email))
            {
                return BadRequest($"An existing account is using {model.Email}, email address. Please use another email address");
            }

            var userToAdd = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Your account has been successfully created.");
        }




        #region Private Helper Methods
        private UserDto CreateApplicationUserDto(User user)
        {
            return new UserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                JWT = _jwtService.CreateJWT(user)
            };
        }
        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
        #endregion
    }
}
