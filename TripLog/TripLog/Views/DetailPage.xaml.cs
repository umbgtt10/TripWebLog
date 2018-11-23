namespace TripLog.Views
{
    using System.ComponentModel;    

    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Xaml;

    using TripLog.ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
    {
        private DetailViewModel _vm
        {
            get { return BindingContext as DetailViewModel; }
            set { BindingContext = value; }
        }

		public DetailPage(BaseViewModel vm)
		{
			InitializeComponent();
		    _vm = (DetailViewModel)vm;
		}

        private void UpdateMap()
        {
            if (_vm.Entry == null)
            {
                return;
            }

            var mapPosition = new Position(_vm.Entry.Latitude, _vm.Entry.Longitude);

            CenterMapOnToPosition(mapPosition);

            SetPinOnMapPosition(mapPosition, _vm.Entry.Title);
        }

	    private void CenterMapOnToPosition(Position position)
	    {
	        map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.5)));
        }

	    private void SetPinOnMapPosition(Position position, string title)
	    {
	        var newPin = new Pin();
	        newPin.Type = PinType.Place;
	        newPin.Label = title;
	        newPin.Position = position;

            map.Pins.Add(newPin);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DetailViewModel.Entry))
            {
                UpdateMap();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null)
            {
                _vm.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (_vm != null)
            {
                _vm.PropertyChanged -= OnViewModelPropertyChanged;
            }
        }
    }
}