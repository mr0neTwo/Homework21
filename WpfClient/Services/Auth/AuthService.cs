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
	public bool IsAuthorized { get; private set; }
	
	private readonly HttpClient _client;

	public AuthService()
	{
		_client = new HttpClient();
		_client.BaseAddress = new Uri("http://localhost:5169/");
		_client.DefaultRequestHeaders.Accept.Clear();
		_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
	}
	
	public async Task<bool> LoginAsync(string username, string password)
	{
		UserLoginModel loginModel = new()
		{
			UserName = username,
			Password = password
		};

		var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
		var response = await _client.PostAsync("auth/login", content);

		if (!response.IsSuccessStatusCode)
		{
			User = null;
			IsAuthorized = false;
			
			return false;
		}

		var responseContent = await response.Content.ReadAsStringAsync();
		LoginResponse? loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

		if (loginResponse == null)
		{
			User = null;
			IsAuthorized = false;
			
			return false;
		}
		
		IsAuthorized = true;
		Token = loginResponse.Token;
		User = loginResponse.User;
		
		return true;
	}
	
	public async Task Register(string username, string password, string confirmPassword)
	{
		await Task.Delay(1000);
	}
}