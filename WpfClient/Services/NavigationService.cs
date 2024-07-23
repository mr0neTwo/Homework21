using WpfClient.ViewModels;

namespace WpfClient.Services;

public sealed class NavigationService : ObservableObject, INavigationService
{
	public ViewModel CurrentView
	{
		get => _currentView;
		private set
		{
			_currentView = value;
			OnPropertyChanged();
		}
	}


	private ViewModel _currentView;
	private readonly Func<Type, ViewModel> _viewModelFactory;

	public NavigationService(Func<Type, ViewModel> viewModelFactory)
	{
		_viewModelFactory = viewModelFactory;
	}

	public void NavigateTo<TViewModel>() where TViewModel : ViewModel
	{
		ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
		viewModel.BeforeShown();
		CurrentView = viewModel;
	}
}
