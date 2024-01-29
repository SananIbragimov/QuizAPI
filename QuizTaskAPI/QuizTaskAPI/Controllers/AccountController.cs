using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizTaskAPI.DTOs.Account;
using QuizTaskAPI.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QuizTaskAPI.Services;

namespace QuizTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!result.Succeeded) return BadRequest("Invalid login attempt");

            var token = GetToken(dto.UserName);

            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var newUser = new AppUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { UserId = newUser.Id });
        }

        private string GetToken(string userName)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddMinutes(5),
                claims: new List<Claim> { new Claim("UserName", userName) },
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
