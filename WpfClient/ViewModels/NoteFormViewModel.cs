using Application.Models;
using WpfClient.Services;
using WpfClient.Services.DataProvider;

namespace WpfClient.ViewModels;

public sealed class NoteFormViewModel(IDataProvider dataProvider, INavigationService navigationService) : ViewModel
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
			await dataProvider.EditNote(Note);
		}
		else
		{
			await dataProvider.AddNote(Note);
		}
		
		navigationService.NavigateTo<NotesViewModel>();
	}
	
	private void Back()
	{
		Note = new Note();
		navigationService.NavigateTo<NotesViewModel>();
	}
}