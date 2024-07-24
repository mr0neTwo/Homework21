using WebAPI.Models;
using WpfClient.Services;
using WpfClient.Services.Auth;

namespace WpfClient.ViewModels;

public sealed class LoginViewModel(IAuthService authService, INavigationService navigationService) : ViewModel
{
	public string UserName
	{
		get => _userName;
		set
		{
			_userName = value;
			OnPropertyChanged();
		}
	}
	
	public string Password
	{
		get => _password;
		set
		{
			_password = value;
			OnPropertyChanged();
		}
	}
	
	public string Error
	{
		get => _error;
		set
		{
			_error = value;
			OnPropertyChanged();
		}
	}

	public DelegateCommand LoginCommand => new(Login);

	private string _userName = string.Empty;
	private string _password = string.Empty;
	private string _error = string.Empty;

	private async void Login(object obj)
	{
		UserLoginModel loginModel = new()
		{
			UserName = UserName,
			Password = Password
		};
		
		AuthResult result = await authService.LoginAsync(loginModel);

		if (result.Success)
		{
			navigationService.NavigateTo<NotesViewModel>();
			ResetForms();
			
			return;
		}
		
		Error = result.ErrorMessage;
	}

	private void ResetForms()
	{
		UserName = string.Empty;
		Password = string.Empty;
	}
}