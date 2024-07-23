namespace WpfClient.ViewModels;

public abstract class ViewModel : ObservableObject
{
	public void BeforeShown()
	{
		OnBeforeShown();
	}

	protected virtual void OnBeforeShown()
	{
	}
}