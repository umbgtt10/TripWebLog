namespace TripLog.Droid.Services
{
    using System;
    using System.Threading.Tasks;

    using Android.Content;
    using Android.Locations;
    using Android.OS;
    using Android.Runtime;

    using TripLog.Models;
    using TripLog.Services;

    public class LocationService : Java.Lang.Object, ILocationService, ILocationListener
    {
        private TaskCompletionSource<Location> _tcs;

        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            try
            {
                var locationManager = (LocationManager) Android.App.Application.Context.GetSystemService(Context.LocationService);

                _tcs = new TaskCompletionSource<Location>();

                locationManager.RequestSingleUpdate(LocationManager.GpsProvider, this, null);

                var location = await _tcs.Task;

                var result = new GeoCoords();
                result.Latitude = location.Latitude;
                result.Longitude = location.Longitude;

                return result;
            }
            catch (Exception e)
            {
                return new GeoCoords();
            }
        }

        public void OnLocationChanged(Location location)
        {
            _tcs.TrySetResult(location);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }
    }
}