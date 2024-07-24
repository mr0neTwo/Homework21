using WebAPI.Models;
using WpfClient.Services;
using WpfClient.Services.Auth;

namespace WpfClient.ViewModels;

public sealed class SignUpViewModel(IAuthService authService, INavigationService navigationService) : ViewModel
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
	
	public string Email
	{
		get => _email;
		set
		{
			_email = value;
			OnPropertyChanged();
		}
	}
	
	public string PhoneNumber
	{
		get => _phoneNumber;
		set
		{
			_phoneNumber = value;
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
	
	public string ConfirmPassword
	{
		get => _confirmPassword;
		set
		{
			_confirmPassword = value;
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
	
	public DelegateCommand RegisterCommand => new(Register);

	private string _userName = string.Empty;
	private string _password = string.Empty;
	private string _email = string.Empty;
	private string _phoneNumber = string.Empty;
	private string _confirmPassword = string.Empty;
	private string _error = string.Empty;

	private async void Register(object obj)
	{
		if (Password != ConfirmPassword)
		{
			Error = "Passwords don't match";
			
			return;
		}
		
		UserRegisterModel registerModel = new()
		{
			UserName = UserName,
			Email = Email,
			PhoneNumber = PhoneNumber,
			Password = Password
		};
		
		AuthResult result = await authService.RegisterAsync(registerModel);

		if (result.Success)
		{
			navigationService.NavigateTo<NotesViewModel>();
			ResetForm();
			
			return;
		}
		
		Error = result.ErrorMessage;
	}

	private void ResetForm()
	{
		UserName = string.Empty;
		Email = string.Empty;
		PhoneNumber = string.Empty;
		Password = string.Empty;
		ConfirmPassword = string.Empty;
		Error = string.Empty;
	}
}
