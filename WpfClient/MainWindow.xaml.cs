using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WpfClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private readonly HttpClient _client;

	public MainWindow()
	{
		InitializeComponent();
		
		_client = new HttpClient();
		_client.BaseAddress = new Uri("http://localhost:5169/");
		_client.DefaultRequestHeaders.Accept.Clear();
		_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	}

	private async void myButton_Click(object sender, RoutedEventArgs e)
	{
		UserLoginModel loginModel = new()
		{
			UserName = "username",
			Password = "password"
		};

		var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
		var response = await _client.PostAsync("auth/login", content);

		if (!response.IsSuccessStatusCode)
		{
			myTextBlock.Text = "Token request failed";
			
			return;
		}

		var responseContent = await response.Content.ReadAsStringAsync();
		string? token = JsonConvert.DeserializeObject<LoginResponse>(responseContent)?.Token;
		
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
		
		HttpResponseMessage dataResponse = await _client.GetAsync("api/protected");
		
		if (!dataResponse.IsSuccessStatusCode)
		{
			myTextBlock.Text = "Data request failed";
			
			return;
		}
		
		string data = await dataResponse.Content.ReadAsStringAsync();
		
		myTextBlock.Text = data;
	}
}
