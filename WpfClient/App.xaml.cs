using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfClient.Services;
using WpfClient.Services.Auth;
using WpfClient.Services.DataProvider;
using WpfClient.ViewModels;
using WpfClient.Views;

namespace WpfClient;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
	private readonly ServiceProvider _serviceProvider;
	
	public App()
	{
		IServiceCollection services = new ServiceCollection();
		
		services.AddSingleton<IDataProvider, ApiDataProvider>();
							  
		services.AddSingleton<MainWindow>
		(
			provider => new MainWindow()
			{
				DataContext = provider.GetRequiredService<MainViewModel>()
			}
		);
		
		services.AddSingleton<MainViewModel>();
		services.AddSingleton<ListViewModel>();
		services.AddSingleton<NoteFormViewModel>();
		services.AddSingleton<LoginViewModel>();

		services.AddSingleton<INavigationService, NavigationService>();
		services.AddSingleton<IAuthService, AuthService>();
		services.AddSingleton<Func<Type, ViewModel>>(provider => viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

		_serviceProvider = services.BuildServiceProvider();
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
		mainWindow.Show();
		
		base.OnStartup(e);
	}
}
