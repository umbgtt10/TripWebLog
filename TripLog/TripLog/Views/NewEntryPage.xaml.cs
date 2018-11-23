namespace TripLog.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using TripLog.ViewModels;    

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewEntryPage : ContentPage
	{
	    private NewEntryViewModel _vm
	    {
	        get { return BindingContext as NewEntryViewModel; }
	        set { BindingContext = value; }
	    }

        public NewEntryPage (BaseViewModel vm)
		{
			InitializeComponent ();
		    _vm = (NewEntryViewModel) vm;
		}
	}
}