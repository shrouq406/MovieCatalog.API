using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieCatalog.API.DTO.AccountDto;
using MovieCatalog.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IConfiguration _config;
        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            _UserManager = userManager;
            _config = config;
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var EmialExist=await _UserManager.FindByEmailAsync(dto.Email);
            if(EmialExist!=null)
            {
                return BadRequest("Email already exists");
            }
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Hobby = dto.Hobby
            };
            var result= await _UserManager.CreateAsync(user,dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("User Register Successfully");
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Ligin(LoginDto dto)
        {
            var user = await _UserManager.FindByNameAsync(dto.Username);

            if(user==null)
            {
                return Unauthorized("Invalid Username or Password");
            }

            var result = await _UserManager.CheckPasswordAsync(user, dto.Password);

            if(!result )
            {
                return Unauthorized("Invalid username or Password");
            }

            //generate JWT token
            var token = GenerateJwtToken(user);
            return Ok(new { Token=token });
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("Name",user.UserName),
                new Claim("Email",user.Email),
                new Claim("Hobby",user.Hobby)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:DurationMinutes"])),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
