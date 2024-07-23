using System.Collections.ObjectModel;
using Application.Models;
using WpfClient.Services;
using WpfClient.Services.DataProvider;

namespace WpfClient.ViewModels;

public sealed class ListViewModel : ViewModel
{
	public ObservableCollection<Note> Notes { get; set; }

	public DelegateCommand EditCommand => new(Edit);
	public DelegateCommand DeleteCommand => new(Delete);

	private readonly IDataProvider _dataProvider;
	private readonly NoteFormViewModel _noteFormViewModel;
	private readonly INavigationService _navigationService;


	public ListViewModel(IDataProvider dataProvider, NoteFormViewModel noteFormViewModel, INavigationService navigationService)
	{
		_dataProvider = dataProvider;
		_noteFormViewModel = noteFormViewModel;
		_navigationService = navigationService;
		LoadNotes();
	}

	protected override void OnBeforeShown()
	{
		LoadNotes();
	}

	private async void LoadNotes()
	{
		List<Note> notes = await _dataProvider.GetAllNotes();
		Notes = new(notes);
		OnPropertyChanged(nameof(Notes));
	}

	private void Edit(object obj)
	{
		if (obj is not Note note)
		{
			return;
		}

		_noteFormViewModel.EditMode = true;
		_noteFormViewModel.Note = note;
		_navigationService.NavigateTo<NoteFormViewModel>();
	}

	private async void Delete(object obj)
	{
		if (obj is not Note note)
		{
			return;
		}
		
		await _dataProvider.DeleteNote(note);
		LoadNotes();
	}
}
