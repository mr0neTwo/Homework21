using System.Collections.ObjectModel;
using Application.Models;
using WpfClient.Services;
using WpfClient.Services.Auth;
using WpfClient.Services.DataProvider;

namespace WpfClient.ViewModels;

public sealed class UsersViewModel : ViewModel
{
	public ObservableCollection<User> Users { get; set; }

	public DelegateCommand EditCommand => new(Edit, CanEdit);
	public DelegateCommand DeleteCommand => new(Delete, CanDelete);

	private readonly IDataProvider _dataProvider;
	private readonly UserFormViewModel _userFormViewModel;
	private readonly INavigationService _navigationService;
	private IAuthService _authService;


	public UsersViewModel(IDataProvider dataProvider, UserFormViewModel userFormViewModel, INavigationService navigationService, IAuthService authService)
	{
		_authService = authService;
		_dataProvider = dataProvider;
		_userFormViewModel = userFormViewModel;
		_navigationService = navigationService;
		LoadUsers();
	}

	protected override void OnBeforeShown()
	{
		LoadUsers();
	}

	private async void LoadUsers()
	{
		List<User> users = await _dataProvider.GetAllUsers();
		Users = new(users);
		OnPropertyChanged(nameof(Users));
	}

	private void Edit(object obj)
	{
		if (obj is not User user)
		{
			return;
		}

		_userFormViewModel.EditMode = true;
		_userFormViewModel.User = user;
		_navigationService.NavigateTo<NoteFormViewModel>();
	}

	private bool CanEdit(object obj)
	{
		return _authService.HasPermission(Permission.CanEditUser);
	}

	private async void Delete(object obj)
	{
		if (obj is not User user)
		{
			return;
		}
		
		await _dataProvider.DeleteUser(user);
		LoadUsers();
	}

	private bool CanDelete(object obj)
	{
		return _authService.HasPermission(Permission.CanDeleteUser);
	}
}
