using Application.Models;
using Database.Data;

namespace WpfClient.Services.DataProvider;

public sealed class TestTestProvider : IDataProvider
{
	private readonly int _delay = 100;

	public async Task<List<Note>> GetAllNotes()
	{
		await Task.Delay(_delay);
		
		return new List<Note>(DefaultValues.Notes());
	}

	public Task AddNote(Note note)
	{
		Console.WriteLine($"Add: {NoteToString(note)}");

		return Task.CompletedTask;
	}

	public Task EditNote(Note note)
	{
		Console.WriteLine($"Edit: {NoteToString(note)}");
		
		return Task.CompletedTask;
	}

	public Task DeleteNote(Note note)
	{
		Console.WriteLine($"Delete: {NoteToString(note)}");
		
		return Task.CompletedTask;
	}

	private string NoteToString(Note note)
	{
		return $"[id: {note.Id}, FirstName: {note.FirstName}, SecondName: {note.SecondName}, ThirdName: {note.ThirdName}, PhoneNumber: {note.PhoneNumber}, Address: {note.Address}, Description: {note.Description}]";
	}
}
