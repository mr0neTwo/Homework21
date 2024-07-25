using System.Collections.ObjectModel;
using Application.Models;
using WebAPI.Models;
using WpfClient.Services;
using WpfClient.Services.DataProvider;

namespace WpfClient.ViewModels;

public sealed class UserFormViewModel(IDataProvider dataProvider, INavigationService navigationService) : ViewModel
{
	public ObservableCollection<Role> Roles { get; set; }
	
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
		LoadRoles();
		
		if (EditMode)
		{
			_user.PasswordHash = string.Empty;
			
			return;
		}

		_user = new();
	}
	
	private async Task LoadRoles()
	{
		List<Role> roles = await dataProvider.GetAllRoles();
		Roles = new ObservableCollection<Role>(roles);
		OnPropertyChanged(nameof(Roles));
	}

	private async void Save()
	{
		UserDto userDto = new()
		{
			Id = User.Id,
			UserName = User.UserName,
			Email = User.Email,
			PhoneNumber = User.PhoneNumber,
			Password = User.PasswordHash,
			RoleId = User.Role.Id
		};
		
		if (EditMode)
		{
			await dataProvider.EditUser(userDto);
		}
		else
		{
			await dataProvider.AddUser(userDto);
		}
		
		navigationService.NavigateTo<UsersViewModel>();
	}
	
	private void Back()
	{
		User = new User();
		navigationService.NavigateTo<UsersViewModel>();
	}
}
