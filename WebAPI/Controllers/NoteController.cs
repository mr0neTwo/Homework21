using Application.Models;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/note/")]
public sealed class NoteController(DatabaseContext database) : Controller
{
	[Authorize, HttpGet("list")]
	public async Task<ActionResult<List<Note>>> GetAllNotes()
	{
		List<Note> listAsync = await database.Notes.ToListAsync();

		return new JsonResult(listAsync);
	}
	
	[Authorize, HttpPost("add")]
	public async Task<ActionResult<int>> Add([FromBody] Note viewModel)
	{
		await database.Notes.AddAsync(viewModel);
		await database.SaveChangesAsync();

		return new JsonResult(viewModel.Id);
	}

	[Authorize, HttpPost("edit")]
	public async Task<IActionResult> Edit([FromBody] Note viewModel)
	{
		Note? note = await database.Notes.FindAsync(viewModel.Id);

		if (note is not null)
		{
			note.FirstName = viewModel.FirstName;
			note.SecondName = viewModel.SecondName;
			note.ThirdName = viewModel.ThirdName;
			note.PhoneNumber = viewModel.PhoneNumber;
			note.Address = viewModel.Address;
			note.Description = viewModel.Description;

			await database.SaveChangesAsync();
		}

		return new JsonResult(string.Empty);
	}
	

	[Authorize, HttpPost("delete")]
	public async Task<IActionResult> Delete([FromBody] Note viewModel)
	{
		Note? note = await database.Notes.AsNoTracking().FirstOrDefaultAsync(note => note.Id == viewModel.Id);

		if (note is not null)
		{
			database.Notes.Remove(note);
			await database.SaveChangesAsync();
		}

		return new JsonResult(string.Empty);
	}
}
