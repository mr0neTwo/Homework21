using Application.Models;
using WpfClient.Services;
using WpfClient.Services.Auth;

namespace WpfClient.ViewModels;

public sealed class MainViewModel(
	INavigationService navigationService,
	NoteFormViewModel noteFormViewModel,
	UserFormViewModel userFormViewModel,
	IAuthService authService) : ViewModel
{
	public INavigationService NavigationService 
	{ 
		get => navigationService;
		set 
		{
			navigationService = value;
			OnPropertyChanged();
		}
	}
	
	public DelegateCommand SigninCommand => new(Signin);
	public DelegateCommand SignUpCommand => new(SignUp);
	public DelegateCommand ShowNotesCommand => new(ShowNotes, CanSeeNotes);
	public DelegateCommand CreateNewNoteCommand => new(CreateNewNote, CanCreateNotes);
	public DelegateCommand ShowUsersCommand => new(ShowUsers, CanSeeUsers);
	public DelegateCommand CreateNewUserCommand => new(CreateNewUser, CanCreateUser);
	

	private void Signin(object obj)
	{
		navigationService.NavigateTo<LoginViewModel>();
	}

	private void SignUp(object obj)
	{
		navigationService.NavigateTo<SignUpViewModel>();
	}

	private void ShowNotes(object obj)
	{
		navigationService.NavigateTo<NotesViewModel>();
	}

	private bool CanSeeNotes(object obj)
	{
		return authService.HasPermission(Permission.CanSeeNotes);
	}

	private void CreateNewNote(object obj)
	{
		noteFormViewModel.EditMode = false;
		navigationService.NavigateTo<NoteFormViewModel>();
	}
	
	private bool CanCreateNotes(object obj)
	{
		return authService.HasPermission(Permission.CanCreateNewNote);
	}

	private void ShowUsers(object obj)
	{
		navigationService.NavigateTo<UsersViewModel>();
	}

	private bool CanSeeUsers(object obj)
	{
		return authService.HasPermission(Permission.CanSeeUsers);
	}
	
	private void CreateNewUser(object obj)
	{
		userFormViewModel.EditMode = false;
		navigationService.NavigateTo<UserFormViewModel>();
	}

	private bool CanCreateUser(object obj)
	{
		return authService.HasPermission(Permission.CanCreateNewUser);
	}
}