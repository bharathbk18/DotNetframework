using Assessmentapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assessmentapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly MoviesDbContext _context;



        public TokenController(IConfiguration config, MoviesDbContext context)
        {
            _configuration = config;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Userdetails _userData)
        {



            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);



                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
new Claim("Id", user.Id.ToString()),
new Claim("Firstname", user.Firstname),
new Claim("Lastname", user.Lastname),
new Claim("Email", user.Email),
};



                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));



                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);



                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        private async Task<Userdetails> GetUser(string email, string password)
        {
            Userdetails userdetails = null;
            var result = await _context.Userdetail.Where(u => u.Email == email && u.Password  == password).ToListAsync();
            if (result.Count > 0)
                userdetails = result[0];
            return userdetails;



        }
    }
}
