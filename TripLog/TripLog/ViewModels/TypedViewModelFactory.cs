namespace TripLog.ViewModels
{ 
    using System;

    public class TypedViewModelFactory
    {
        private readonly ViewModelFactory _factory;

        public TypedViewModelFactory(ViewModelFactory factory)
        {
            _factory = factory;
        }

        public BaseViewModel BuildViewModel(ViewModelType type)
        {
            switch (type)
            {
                case ViewModelType.Detail:
                    return _factory.BuildDetailViewModel();

                case ViewModelType.Main:
                    return _factory.BuildMainViewModel();

                case ViewModelType.New:
                    return _factory.BuildNewEntryViewModel();

                default:
                    throw new Exception("TBD");
            }
        }
    }
}
