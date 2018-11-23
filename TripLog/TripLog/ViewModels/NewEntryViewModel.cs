namespace TripLog.ViewModels
{
    using System;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    using TripLog.Models;
    using TripLog.Services;    

    public class NewEntryViewModel : BaseViewModel
    {
        #region Observables and commands

        string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        int _rating;
        public int Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }

        string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        private Command _saveCommand;
        public Command SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new Command(async () => await ExecuteSaveCommand(), CanSave);                    
                }
                return _saveCommand;
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        private async Task ExecuteSaveCommand()
        {
            var newEntry = new TripLogEntry();

            newEntry.Title = Title;
            newEntry.Notes = Notes;
            newEntry.Rating = Rating;
            newEntry.Date = Date;
            newEntry.Latitude = Latitude;
            newEntry.Longitude = Longitude;

            await _tripLogService.AddEntryAsync(newEntry);

            await NavService.GoBack();
        }

        #endregion

        private readonly ILocationService _locService;
        private readonly ITripLogDataService _tripLogService;

        public NewEntryViewModel(ILocationService locService,
                                 ITripLogDataService tripLogService)
        {
            _locService = locService;
            _tripLogService = tripLogService;
            Date = DateTime.Today;
            Rating = 1;
        }

        public override async Task Init()
        {
            var coordinates = await _locService.GetGeoCoordinatesAsync();
            Latitude = coordinates.Latitude;
            Longitude = coordinates.Longitude;
        }

        public override Task Init(TripLogEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
