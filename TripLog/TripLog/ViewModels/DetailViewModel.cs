namespace TripLog.ViewModels
{
    using System.Threading.Tasks;

    using TripLog.Models;

    public class DetailViewModel : BaseViewModel
    {
        private TripLogEntry _entry;

        public TripLogEntry Entry
        {
            get { return _entry; }
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public DetailViewModel(){}

        public override async Task Init(TripLogEntry entry)
        {
            Entry = entry;
        }

        public override async Task Init()
        {
            throw new EntryNotProvidedException();
        }
    }
}
