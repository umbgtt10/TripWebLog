using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Ninject.Modules;
using TripLog.iOS.Services;
using TripLog.Services;
using UIKit;

namespace TripLog.iOS.Module
{
    public class TripLogPlatformModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>().InSingletonScope();
        }
    }
}