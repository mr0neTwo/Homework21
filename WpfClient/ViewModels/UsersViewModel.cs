using System.Collections.ObjectModel;
using Application.Models;
using WebAPI.Models;
using WpfClient.Services;
using WpfClient.Services.Auth;
using WpfClient.Services.DataProvider;

namespace WpfClient.ViewModels;

public sealed class UsersViewModel(
	IDataProvider dataProvider,
	UserFormViewModel userFormViewModel,
	INavigationService navigationService,
	IAuthService authService) : ViewModel
{
	public ObservableCollection<User> Users { get; set; }

	public DelegateCommand EditCommand => new(Edit, CanEdit);
	public DelegateCommand DeleteCommand => new(Delete, CanDelete);


	protected override void OnBeforeShown()
	{
		LoadUsers();
	}

	private async void LoadUsers()
	{
		List<User> users = await dataProvider.GetAllUsers();
		Users = new(users);
		OnPropertyChanged(nameof(Users));
	}

	private void Edit(object obj)
	{
		if (obj is not User user)
		{
			return;
		}

		userFormViewModel.EditMode = true;
		userFormViewModel.User = user;
		navigationService.NavigateTo<UserFormViewModel>();
	}

	private bool CanEdit(object obj)
	{
		return authService.HasPermission(Permission.CanEditUser);
	}

	private async void Delete(object obj)
	{
		if (obj is not User user)
		{
			return;
		}

		UserDto userDto = new() { Id = user.Id };
		
		await dataProvider.DeleteUser(userDto); 
		LoadUsers();
	}

	private bool CanDelete(object obj)
	{
		return authService.HasPermission(Permission.CanDeleteUser);
	}
}
