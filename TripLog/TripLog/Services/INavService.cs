namespace TripLog.Services
{
    using System.ComponentModel;
    using System.Threading.Tasks;

    using TripLog.Models;
    using TripLog.ViewModels;

    public interface TypedNavService
    {
        bool CanGoBack { get; }
        Task GoBack();
        Task NavigateTo(ViewModelType viewModelType);
        Task NavigateTo(ViewModelType viewModelType, TripLogEntry entry);

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
