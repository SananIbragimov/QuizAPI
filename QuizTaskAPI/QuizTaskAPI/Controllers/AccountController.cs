using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizTaskAPI.DTOs.Account;
using QuizTaskAPI.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using System.Data;

namespace QuizTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid login data");
            }

            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!result.Succeeded) return BadRequest("Invalid login attempt");
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return BadRequest("Invalid login attempt");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = GetToken(dto.UserName, roles);

            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid registration data");
            }

            var newUser = _mapper.Map<AppUser>(dto);

            var result = await _userManager.CreateAsync(newUser, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }


            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }


            await _userManager.AddToRoleAsync(newUser, "User");


            if (IsAdmin(newUser) && !await _userManager.IsInRoleAsync(newUser, "Admin"))
            {

                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                await _userManager.AddToRoleAsync(newUser, "Admin");
            }

            return Ok(new { UserId = newUser.Id });
        }


        private bool IsAdmin(AppUser user)
        {
            return user.UserName.Contains("admin", StringComparison.OrdinalIgnoreCase);
        }

        private string GetToken(string userName, IEnumerable<string> roles)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var claims = new List<Claim>
            {
                new Claim("UserName", userName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
