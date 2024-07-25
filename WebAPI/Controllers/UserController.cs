using Application.Models;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/user/")]
public sealed class UserController(DatabaseContext database) : Controller
{
	[Authorize, HttpGet("list")]
	public async Task<ActionResult<List<User>>> GetAllUsers()
	{
		List<User> listAsync = await database.Users.Include(user => user.Role).ToListAsync();

		return new JsonResult(listAsync);
	}
	
	[Authorize, HttpPost("add")]
	public async Task<ActionResult<int>> Add([FromBody] UserDto userModel)
	{
		User user = new()
		{
			UserName = userModel.UserName ?? string.Empty,
			Email = userModel.Email ?? string.Empty,
			PhoneNumber = userModel.PhoneNumber ?? string.Empty,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(userModel.Password),
			RoleId = userModel.RoleId,
		};
		
		await database.Users.AddAsync(user);
		await database.SaveChangesAsync();

		return new JsonResult(userModel.Id);
	}

	[Authorize, HttpPost("edit")]
	public async Task<IActionResult> Edit([FromBody] UserDto userModel)
	{
		User? user = await database.Users.FindAsync(userModel.Id);

		if (user is not null)
		{
			user.UserName = userModel.UserName ?? user.UserName;
			user.Email = userModel.Email ?? user.Email;
			user.PhoneNumber = userModel.PhoneNumber ?? user.PhoneNumber;
			user.RoleId = userModel.RoleId != 0 ? userModel.RoleId : user.RoleId;
			user.PasswordHash = userModel.Password != null ? BCrypt.Net.BCrypt.HashPassword(userModel.Password) : user.PasswordHash;

			await database.SaveChangesAsync();
		}

		return new JsonResult(string.Empty);
	}
	

	[Authorize, HttpPost("delete")]
	public async Task<IActionResult> Delete([FromBody] UserDto userModel)
	{
		User? user = await database.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == userModel.Id);

		if (user is not null)
		{
			database.Users.Remove(user);
			await database.SaveChangesAsync();
		}

		return new JsonResult(string.Empty);
	}
}
