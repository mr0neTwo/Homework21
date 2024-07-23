using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Models;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("auth/")]
public sealed class AuthController(IConfiguration configuration, DatabaseContext context) : Controller
{
	private readonly IConfiguration _configuration = configuration;
	private readonly DatabaseContext _context = context;

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
	{
		User? user = await _context.Users
								   .Include(user => user.Role)
								   .FirstOrDefaultAsync(user => user.UserName == userLoginModel.UserName);

		if (user == null)
		{
			LoginResponse badResponse = new() { ErrorMessage = "Incorrect UserName or Password" };
			
			return BadRequest(badResponse);
		}

		LoginResponse loginResponse = new()
		{
			Token = GenerateToken(userLoginModel.UserName),
			User = user
		};

		return Ok(loginResponse);
	}
	
	private string GenerateToken(string userName)
	{
		SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

		Claim[] claims =
		{
			new(JwtRegisteredClaimNames.Sub, userName),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		JwtSecurityToken token = new
		(
			issuer : _configuration["Jwt:Issuer"],
			audience : _configuration["Jwt:Audience"],
			claims : claims, 
			expires : DateTime.UtcNow.AddHours(48),
			signingCredentials : credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
