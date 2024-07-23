namespace WpfClient.Services.Auth;

public interface IAuthService
{
	string? Token { get; }
	
	Task<bool> LoginAsync(string username, string password);

	Task Register(string username, string password, string confirmPassword);
}
