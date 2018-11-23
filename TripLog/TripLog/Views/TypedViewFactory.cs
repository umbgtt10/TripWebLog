namespace TripLog.Views
{
    using System;

    using Xamarin.Forms;

    using TripLog.ViewModels;    

    public class TypedViewFactory
    {
        private readonly TypedViewModelFactory _viewModelFactory;

        public TypedViewFactory(TypedViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ContentPage BuildView(ViewModelType type)
        {
            var vm = _viewModelFactory.BuildViewModel(type);

            switch (type)
            {
                case ViewModelType.Detail:
                    return new DetailPage(vm);

                case ViewModelType.Main:
                    return new MainPage(vm);

                case ViewModelType.New:
                    return new NewEntryPage(vm);

                default:
                    throw new Exception("TBD");
            }
        }
    }
}
