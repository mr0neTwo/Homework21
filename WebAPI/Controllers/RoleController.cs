using Application.Models;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/role/")]
public sealed class RoleController(DatabaseContext database) : Controller
{
	[Authorize, HttpGet("list")]
	public async Task<ActionResult<List<User>>> GetAllRoles()
	{
		List<Role> listAsync = await database.Roles.ToListAsync();

		return new JsonResult(listAsync);
	}
}
