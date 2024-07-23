using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("auth/")]
public sealed class AuthController(IConfiguration configuration) : Controller
{
	private IConfiguration _configuration = configuration;

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
	{
		await Task.Delay(100);
		LoginResponse loginResponse = new() { Token = GenerateToken(userLoginModel.UserName) };

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
