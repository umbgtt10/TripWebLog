namespace TripLog.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Akavache;
    using Xamarin.Forms;
    
    using TripLog.Models;
    using TripLog.Services;

    public class MainViewModel: BaseViewModel
    {
        #region Observables and commands
        private ObservableCollection<TripLogEntry> _logEntries;
        public ObservableCollection<TripLogEntry> LogEntries
        {
            get { return _logEntries; }
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        private Command<TripLogEntry> _viewCommand;
        public Command<TripLogEntry> ViewCommand
        {
            get
            {
                if (_viewCommand == null)
                {
                    _viewCommand = new Command<TripLogEntry>(async (entry) => await ExecuteViewCommand(entry));
                }
                return _viewCommand;
            }
        }

        private Command _newCommand;
        public Command NewCommand
        {
            get
            {
                if (_newCommand == null)
                {
                    _newCommand = new Command(async () => await ExecuteNewCommand());
                }
                return _newCommand;
            }
        }
        #endregion

        private readonly ITripLogDataService _tripLogService;
        private readonly IBlobCache _cache;

        public MainViewModel(ITripLogDataService tripLogService,
                             IBlobCache cache)
        {
            LogEntries = new ObservableCollection<TripLogEntry>();

            _tripLogService = tripLogService;
            _cache = cache;
        }

        private async Task ExecuteViewCommand(TripLogEntry entry)
        {
            await NavService.NavigateTo(ViewModelType.Detail, entry);
        }

        private async Task ExecuteNewCommand()
        {
            await NavService.NavigateTo(ViewModelType.New);
        }

        public override async Task Init()
        {
            LoadEntries();
        }

        private async void LoadEntries()
        {
            try
            {
                _cache.GetAndFetchLatest("entries", PullEntriesFromService())
                    .Subscribe(entries => LogEntries = new ObservableCollection<TripLogEntry>(entries));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private Func<Task<IList<TripLogEntry>>> PullEntriesFromService()
        {
            return async () => await _tripLogService.GetEntriesAsync();
        }

        public override Task Init(TripLogEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
