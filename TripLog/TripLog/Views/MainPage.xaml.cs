namespace TripLog.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using TripLog.Models;
    using TripLog.ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
	    private MainViewModel _vm
	    {
	        get { return BindingContext as MainViewModel; }
	        set { BindingContext = value; }
	    }

	    public MainPage()
	    {
	        InitializeComponent();
        }

	    public MainPage(BaseViewModel vm) : this()
		{			
		    _vm = (MainViewModel) vm;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null)
            {
                await _vm.Init();
            }
        }

	    private void OnListItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var trip = (TripLogEntry) e.Item;
            _vm.ViewCommand.Execute(trip);

	        trips.SelectedItem = null;
	    }
	}
}