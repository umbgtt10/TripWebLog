using System;
using Akavache;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;

namespace TripLog
{
    public class TripLogFactories
    {
        private readonly ILocationService _locationService;
        private readonly INavigation _xamarinFormsNavigation;

        public TypedViewFactory TypedViewFactory { get; private set; }
        public TypedViewModelFactory TypedViewModelFactory { get; private set; }
        public TypedXamarinFormsNavService NavigationService { get; private set; }

        public TripLogFactories(ILocationService locationService, INavigation xamarinFormsNavigation)
        {
            _locationService = locationService;
            _xamarinFormsNavigation = xamarinFormsNavigation;
        }

        public void Build()
        {
            var uri = new Uri("http://192.168.1.21:30080/api/TripLogWeb/");

            var tripLogService = new TripLogEntryDataService(uri);

            BlobCache.ApplicationName = "TripLogWeb";
            BlobCache.EnsureInitialized();

            var viewModelFactory = new ViewModelFactory(_locationService, tripLogService, BlobCache.LocalMachine);
            TypedViewModelFactory = new TypedViewModelFactory(viewModelFactory);
            TypedViewFactory = new TypedViewFactory(TypedViewModelFactory);
            NavigationService = new TypedXamarinFormsNavService(TypedViewFactory, _xamarinFormsNavigation);
            viewModelFactory.Update(NavigationService);
        }
    }
}
