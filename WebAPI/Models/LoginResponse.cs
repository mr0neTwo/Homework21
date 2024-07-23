using Application.Models;

namespace WebAPI.Models;

public sealed class LoginResponse
{
	public User User { get; set; }
	public string Token { get; set; }
	public string ErrorMessage { get; set; }
}
