using WpfClient.Services;
using WpfClient.Services.Auth;

namespace WpfClient.ViewModels;

public sealed class LoginViewModel : ViewModel
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

	public DelegateCommand LoginCommand => new(Login);

	private string _userName;
	private string _password;
	
	private INavigationService _navigationService;
	private readonly IAuthService _authService;

	public LoginViewModel(IAuthService authService, INavigationService navigationService)
	{
		_navigationService = navigationService;
		_authService = authService;
	}

	private async void Login(object obj)
	{
		bool isLogin = await _authService.LoginAsync(UserName, Password);

		if (isLogin)
		{
			_navigationService.NavigateTo<ListViewModel>();
		}
	}
}