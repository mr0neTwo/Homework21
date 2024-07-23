using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/")]
public sealed class ApiController : Controller
{
	[Route("protected")]
	[Authorize]
	public IActionResult Protected()
	{
		string protectedData = "protected data";
		
		return Ok(protectedData);
	}
}
