using WpfClient.Services;

namespace WpfClient.ViewModels;

public sealed class MainViewModel : ViewModel
{
	public INavigationService NavigationService 
	{ 
		get => _navigationService;
		set 
		{
			_navigationService = value;
			OnPropertyChanged();
		}
	}


	public DelegateCommand SigninCommand => new(Signin);
	public DelegateCommand SignOutCommand => new(Signout);
	public DelegateCommand ShowListCommand => new(ShowList);
	public DelegateCommand AddNewCommand => new(AddNew);

	private INavigationService _navigationService;
	private NoteFormViewModel _noteFormViewModel;

	public MainViewModel(INavigationService navigationService, NoteFormViewModel noteFormViewModel)
	{
		_noteFormViewModel = noteFormViewModel;
		NavigationService = navigationService;
	}


	private void Signin(object obj)
	{
		_navigationService.NavigateTo<LoginViewModel>();
	}

	private void Signout(object obj)
	{
		Console.WriteLine("Sign out");
	}

	private void ShowList(object obj)
	{
		_navigationService.NavigateTo<ListViewModel>();
	}

	private void AddNew(object obj)
	{
		_noteFormViewModel.EditMode = false;
		_navigationService.NavigateTo<NoteFormViewModel>();
	}
}