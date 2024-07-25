using System.Text.Json.Serialization;

namespace Application.Models;

public sealed class User
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public string PasswordHash { get; set; }
	public int RoleId { get; set; }
	
	public Role Role { get; set; }
}
