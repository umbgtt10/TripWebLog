namespace TripLog.ViewModels
{
    using Akavache;

    using TripLog.Services;

    public class ViewModelFactory
    {
        private readonly ILocationService _locService;
        private readonly ITripLogDataService _dataService;
        private readonly IBlobCache _cache;
        private TypedNavService _navigationService;

        public ViewModelFactory(ILocationService locService, ITripLogDataService dataService, IBlobCache cache)
        {
            _locService = locService;
            _dataService = dataService;
            _cache = cache;
        }

        public void Update(TypedNavService navigationService)
        {
            _navigationService = navigationService;
        }

        public BaseViewModel BuildMainViewModel()
        {
            var result = new MainViewModel(_dataService, _cache);
            result.SetNavigationService(_navigationService);

            return result;
        }

        public BaseViewModel BuildNewEntryViewModel()
        {
            var result = new NewEntryViewModel(_locService, _dataService);
            result.SetNavigationService(_navigationService);

            return result;
        }

        public BaseViewModel BuildDetailViewModel()
        {
            var result = new DetailViewModel();
            result.SetNavigationService(_navigationService);

            return result;
        }
    }
}
