using WpfClient.ViewModels;

namespace WpfClient.Services;

public interface INavigationService
{
	public ViewModel CurrentView { get; }

	public void NavigateTo<T>() where T : ViewModel;
}