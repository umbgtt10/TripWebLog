namespace TripLog.UWP.Modules
{
    using Ninject.Modules;

    using TripLog.Services;

    public class TripLogPlatformModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>().InSingletonScope();
        }
    }
}
