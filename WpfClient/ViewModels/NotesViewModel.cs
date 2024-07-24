using System.Collections.ObjectModel;
using Application.Models;
using WpfClient.Services;
using WpfClient.Services.Auth;
using WpfClient.Services.DataProvider;

namespace WpfClient.ViewModels;

public sealed class NotesViewModel : ViewModel
{
	public ObservableCollection<Note> Notes { get; set; }

	public DelegateCommand EditCommand => new(Edit, obj => _authService.HasPermission(Permission.CanEditNote));
	public DelegateCommand DeleteCommand => new(Delete, obj => _authService.HasPermission(Permission.CanDeleteNote));

	private readonly IDataProvider _dataProvider;
	private readonly NoteFormViewModel _noteFormViewModel;
	private readonly INavigationService _navigationService;
	private IAuthService _authService;


	public NotesViewModel(IDataProvider dataProvider, NoteFormViewModel noteFormViewModel, INavigationService navigationService, IAuthService authService)
	{
		_authService = authService;
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
