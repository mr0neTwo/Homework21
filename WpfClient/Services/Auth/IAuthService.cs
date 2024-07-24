using Application.Models;
using WebAPI.Models;

namespace WpfClient.Services.Auth;

public interface IAuthService
{
	public User? User { get; }
	string? Token { get; }
	
	public Task<AuthResult> LoginAsync(UserLoginModel userLoginModel);

	public Task<AuthResult> RegisterAsync(UserRegisterModel userRegisterModel);

	public bool HasPermission(Permission permission);
}
