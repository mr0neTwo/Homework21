using Application.Models;
using WpfClient.Services;
using WpfClient.Services.DataProvider;

namespace WpfClient.ViewModels;

public class UserFormViewModel(IDataProvider dataProvider, INavigationService navigationService) : ViewModel
{
	public User User
	{
		get => _user;
		set
		{
			_user = value;
			OnPropertyChanged();
		}
	}
	
	public bool EditMode { get; set; }

	public DelegateCommand SaveCommand => new(execute => Save());
	public DelegateCommand BackCommand => new(execute => Back());

	private User _user = new();

	protected override void OnBeforeShown()
	{
		if (EditMode)
		{
			return;
		}

		_user = new();
	}

	private async void Save()
	{
		if (EditMode)
		{
			await dataProvider.EditUser(User);
		}
		else
		{
			await dataProvider.AddUser(User);
		}
		
		navigationService.NavigateTo<UsersViewModel>();
	}
	
	private void Back()
	{
		User = new User();
		navigationService.NavigateTo<UsersViewModel>();
	}
}
