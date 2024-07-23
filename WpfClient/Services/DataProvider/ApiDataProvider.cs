using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using WpfClient.Services.Auth;

namespace WpfClient.Services.DataProvider;

public sealed class ApiDataProvider : IDataProvider
{
	private readonly HttpClient _client;
	private readonly IAuthService _authService;

	public ApiDataProvider(IAuthService authService)
	{
		_authService = authService;
		_client = new HttpClient();
		
		_client.BaseAddress = new Uri("http://localhost:5169/api/");
		
		_client.DefaultRequestHeaders.Accept.Clear();
		_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	}

	public async Task<List<Note>> GetAllNotes()
	{
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, _authService.Token);
		HttpResponseMessage response = await _client.GetAsync("list");

		if (response.IsSuccessStatusCode == false)
		{
			return new List<Note>();
		}
		
		string json = await response.Content.ReadAsStringAsync();
		List<Note>? notes = JsonConvert.DeserializeObject<List<Note>>(json);

		if (notes == null)
		{
			return new List<Note>();
		}
		
		return notes;
	}

	public async Task AddNote(Note note)
	{
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, _authService.Token);
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.Default, "application/json");
		await _client.PostAsync("add", content);
	}

	public async Task EditNote(Note note)
	{
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		await _client.PostAsync("edit", content);
	}

	public async Task DeleteNote(Note note)
	{
		string json = JsonConvert.SerializeObject(note);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		await _client.PostAsync("delete", content);
	}
}