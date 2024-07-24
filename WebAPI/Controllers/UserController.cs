using Application.Models;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/user/")]
public sealed class UserController(DatabaseContext database) : Controller
{
	[Authorize, HttpGet("list")]
	public async Task<ActionResult<List<User>>> GetAllNotes()
	{
		List<User> listAsync = await database.Users.Include(user => user.Role).ToListAsync();

		return new JsonResult(listAsync);
	}
	
	[Authorize, HttpPost("add")]
	public async Task<ActionResult<int>> Add([FromBody] User userModel)
	{
		await database.Users.AddAsync(userModel);
		await database.SaveChangesAsync();

		return new JsonResult(userModel.Id);
	}

	[Authorize, HttpPost("edit")]
	public async Task<IActionResult> Edit([FromBody] User userModel)
	{
		User? user = await database.Users.FindAsync(userModel.Id);

		if (user is not null)
		{
			user.UserName = userModel.UserName;
			user.Email = userModel.Email;
			user.PhoneNumber = userModel.PhoneNumber;
			user.RoleId = userModel.RoleId;

			await database.SaveChangesAsync();
		}

		return new JsonResult(string.Empty);
	}
	

	[Authorize, HttpPost("delete")]
	public async Task<IActionResult> Delete([FromBody] User userModel)
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
