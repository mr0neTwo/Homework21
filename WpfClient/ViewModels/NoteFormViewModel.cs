using Application.Models;
using WpfClient.Services;
using WpfClient.Services.DataProvider;

namespace WpfClient.ViewModels;

public sealed class NoteFormViewModel : ViewModel
{
	public Note Note
	{
		get => _note;
		set
		{
			_note = value;
			OnPropertyChanged();
		}
	}
	
	public bool EditMode { get; set; }

	public DelegateCommand SaveCommand => new(execute => Save());
	public DelegateCommand BackCommand => new(execute => Back());

	private Note _note = new Note();

	private readonly IDataProvider _dataProvider;
	private readonly INavigationService _navigationService;

	public NoteFormViewModel(IDataProvider dataProvider, INavigationService navigationService)
	{
		_dataProvider = dataProvider;
		_navigationService = navigationService;
	}

	protected override void OnBeforeShown()
	{
		if (EditMode)
		{
			return;
		}

		_note = new();
	}

	private async void Save()
	{
		if (EditMode)
		{
			await _dataProvider.EditNote(Note);
		}
		else
		{
			await _dataProvider.AddNote(Note);
		}
		
		_navigationService.NavigateTo<ListViewModel>();
	}
	
	private void Back()
	{
		Note = new Note();
		_navigationService.NavigateTo<ListViewModel>();
	}
}