using System.Net.Http;
using System.Text;
using Application.Models;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WpfClient.Services.Auth;

public sealed class AuthService : IAuthService
{
	public User? User { get; private set; }
	public string? Token { get; private set; }
	
	private readonly HttpClient _client;

	public AuthService()
	{
		_client = new HttpClient();
		_client.BaseAddress = new Uri("http://localhost:5169/");
		_client.DefaultRequestHeaders.Accept.Clear();
		_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
	}
	
	public async Task<AuthResult> LoginAsync(UserLoginModel userLoginModel)
	{
		StringContent content = new(JsonConvert.SerializeObject(userLoginModel), Encoding.UTF8, "application/json");
		HttpResponseMessage response = await _client.PostAsync("auth/login", content);
		
		string responseContent = await response.Content.ReadAsStringAsync();
		AuthResponse? loginResponse = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

		if (!response.IsSuccessStatusCode)
		{
			User = null;
			
			return new AuthResult()
			{
				Success = false,
				ErrorMessage = loginResponse?.ErrorMessage ?? "Bad request"
			};
		}

		if (loginResponse.User == null)
		{
			return new AuthResult()
			{
				Success = false,
				ErrorMessage = loginResponse.ErrorMessage
			};
		}
		
		
		Token = loginResponse.Token;
		User = loginResponse.User;
		
		return new AuthResult()
		{
			Success = true
		};
	}
	
	public async Task<AuthResult> RegisterAsync(UserRegisterModel userRegisterModel)
	{
		StringContent content = new(JsonConvert.SerializeObject(userRegisterModel), Encoding.UTF8, "application/json");
		HttpResponseMessage response = await _client.PostAsync("auth/register", content);
		
		string responseContent = await response.Content.ReadAsStringAsync();
		AuthResponse? loginResponse = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

		if (!response.IsSuccessStatusCode)
		{
			User = null;
			
			return new AuthResult()
			{
				Success = false,
				ErrorMessage = loginResponse?.ErrorMessage ?? "Bad request"
			};
		}
		
		if (loginResponse.User == null)
		{
			return new AuthResult()
			{
				Success = false,
				ErrorMessage = loginResponse.ErrorMessage
			};
		}
		
		Token = loginResponse.Token;
		User = loginResponse.User;
		
		return new AuthResult()
		{
			Success = true
		};
	}

	public bool HasPermission(Permission permission)
	{
		return User?.Role.Permissions.Contains(permission) ?? false;
	}
}