using Application.Models;

namespace WpfClient.Services.DataProvider;

public interface IDataProvider
{
	public Task<List<Note>> GetAllNotes();
	
	public Task AddNote(Note note);

	public Task EditNote(Note note);

	public Task DeleteNote(Note note);

	public Task<List<User>> GetAllUsers();
	
	public Task AddUser(User user);
	
	public Task EditUser(User user);
	
	public Task DeleteUser(User user);
}