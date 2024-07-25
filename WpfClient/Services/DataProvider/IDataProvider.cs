using Application.Models;
using WebAPI.Models;

namespace WpfClient.Services.DataProvider;

public interface IDataProvider
{
	public Task<List<Note>> GetAllNotes();
	
	public Task AddNote(Note note);

	public Task EditNote(Note note);

	public Task DeleteNote(Note note);

	public Task<List<User>> GetAllUsers();
	
	public Task AddUser(UserDto user);
	
	public Task EditUser(UserDto user);
	
	public Task DeleteUser(UserDto user);

	public Task<List<Role>> GetAllRoles();
}