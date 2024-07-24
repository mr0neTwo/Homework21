using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Models;
using Database;
using Database.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("auth/")]
public sealed class AuthController(IConfiguration configuration, DatabaseContext database) : Controller
{
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
	{
		User? user = await database.Users
								   .Include(user => user.Role)
								   .FirstOrDefaultAsync(user => user.UserName == userLoginModel.UserName);

		if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginModel.Password, user.PasswordHash))
		{
			AuthResponse badResponse = new() { ErrorMessage = "Incorrect UserName or Password" };
			
			return BadRequest(badResponse);
		}

		AuthResponse authResponse = new()
		{
			Token = GenerateToken(userLoginModel.UserName),
			User = user
		};

		return Ok(authResponse);
	}
	
	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] UserRegisterModel userRegisterModel)
	{
		User? user = await database.Users.FirstOrDefaultAsync(user => user.UserName == userRegisterModel.UserName);

		if (user != null)
		{
			AuthResponse badResponse = new() { ErrorMessage = "User with that name has already been registered" };
			
			return BadRequest(badResponse);
		}

		Role userRole = await database.Roles.FirstOrDefaultAsync(role => role.Name == DefaultValues.UserRole.Name);

		User newUser = new()
		{
			UserName = userRegisterModel.UserName,
			Email = userRegisterModel.Email,
			PhoneNumber = userRegisterModel.PhoneNumber,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterModel.Password),
			RoleId = userRole.Id
		};

		await database.Users.AddAsync(newUser);
		await database.SaveChangesAsync();

		AuthResponse authResponse = new()
		{
			Token = GenerateToken(userRegisterModel.UserName),
			User = newUser
		};

		return Ok(authResponse);
	}
	
	private string GenerateToken(string userName)
	{
		SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
		SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

		Claim[] claims =
		{
			new(JwtRegisteredClaimNames.Sub, userName),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		JwtSecurityToken token = new
		(
			issuer : configuration["Jwt:Issuer"],
			audience : configuration["Jwt:Audience"],
			claims : claims, 
			expires : DateTime.UtcNow.AddHours(48),
			signingCredentials : credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
