
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TripLog
{
    using Xamarin.Forms;
    using Ninject;
    using Ninject.Modules;

    using TripLog.Services;
    using TripLog.ViewModels;
    using TripLog.Views;

    public partial class App : Application
    {
        public IKernel Kernel { get; set; }
        public TripLogFactories Factories { get; private set; }

        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();

            var mainPage = new NavigationPage(new MainPage());

            Kernel = new StandardKernel();

            Kernel.Load(platformModules);

            Factories = new TripLogFactories(Kernel.Get<ILocationService>(), mainPage.Navigation);

            Factories.Build();

            mainPage.BindingContext = Factories.TypedViewModelFactory.BuildViewModel(ViewModelType.Main);

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
