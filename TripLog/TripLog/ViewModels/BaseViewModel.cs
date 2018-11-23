namespace TripLog.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using TripLog.Models;
    using TripLog.Services;

    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected TypedNavService NavService { get; private set; }

        protected BaseViewModel() {}

        public void SetNavigationService(TypedNavService navService)
        {
            NavService = navService;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public abstract Task Init();

        public abstract Task Init(TripLogEntry entry);
    }
}
