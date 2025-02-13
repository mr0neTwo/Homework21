using Application.Models;
using Database.Data;
using WebAPI.Models;

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

	public async Task<List<User>> GetAllUsers()
	{
		await Task.Delay(100);
		
		return [DefaultValues.Admin, DefaultValues.User];
	}

	public Task AddUser(UserDto user)
	{
		Console.WriteLine($"Add: {UserToString(user)}");

		return Task.CompletedTask;
	}

	public Task EditUser(UserDto user)
	{
		Console.WriteLine($"Edit: {UserToString(user)}");
		
		return Task.CompletedTask;
	}

	public Task DeleteUser(UserDto user)
	{
		Console.WriteLine($"Delete: {UserToString(user)}");
		
		return Task.CompletedTask;
	}

	private static string NoteToString(Note note)
	{
		return $"[id: {note.Id}, FirstName: {note.FirstName}, SecondName: {note.SecondName}, ThirdName: {note.ThirdName}, PhoneNumber: {note.PhoneNumber}, Address: {note.Address}, Description: {note.Description}]";
	}
	
	private static string UserToString(UserDto user)
	{
		return $"[id: {user.Id}, UserName: {user.UserName}, Email: {user.Email}, PhoneNumber: {user.PhoneNumber}, Role: {user.RoleId}]";
	}
	
	public async Task<List<Role>> GetAllRoles()
	{
		await Task.Delay(100);
		
		return [DefaultValues.AdminRole, DefaultValues.UserRole];
	}
}
