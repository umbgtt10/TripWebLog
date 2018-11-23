namespace TripLog.Services
{
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    using TripLog.Models;
    using TripLog.ViewModels;
    using TripLog.Views;    

    public class TypedXamarinFormsNavService : TypedNavService
    {
        public INavigation _xamarinFormsNavigation;
        private readonly TypedViewFactory _typedViewFactory;

        public bool CanGoBack
        {
            get
            {
                return _xamarinFormsNavigation.NavigationStack != null && _xamarinFormsNavigation.NavigationStack.Count > 0;
            }
        }

        public event PropertyChangedEventHandler CanGoBackChanged;

        public TypedXamarinFormsNavService(TypedViewFactory typedViewFactory, INavigation xamarinFormsNavigation)
        {
            _typedViewFactory = typedViewFactory;
            _xamarinFormsNavigation = xamarinFormsNavigation;
        }

        public async Task GoBack()
        {
            if (CanGoBack)
            {
                await _xamarinFormsNavigation.PopAsync(true);
            }

            OnCanGoBackChanged();
        }

        public async Task NavigateTo(ViewModelType viewModelType)
        {
            await NavigateToView(viewModelType);

            await PullViewModelFromStack().Init();
        }

        public async Task NavigateTo(ViewModelType viewModelType, TripLogEntry entry)
        {
            await NavigateToView(viewModelType);

            await PullViewModelFromStack().Init(entry);
        }

        private void OnCanGoBackChanged()
        {
            CanGoBackChanged?.Invoke(this, new PropertyChangedEventArgs("CanGoBack"));
        }

        private async Task NavigateToView(ViewModelType viewModelType)
        {
            var view = _typedViewFactory.BuildView(viewModelType);

            await _xamarinFormsNavigation.PushAsync(view, true);
        }

        private BaseViewModel PullViewModelFromStack()
        {
            return (BaseViewModel)_xamarinFormsNavigation.NavigationStack.Last().BindingContext;
        }
    }
}
