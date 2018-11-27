namespace TripLog.UWP.Modules
{
    using System;
    using System.Threading.Tasks;

    using TripLog.Models;
    using TripLog.Services;

    using Windows.Devices.Geolocation;

    public class LocationService : ILocationService
    {
        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            var locator = new Geolocator();
            var coordinates = await locator.GetGeopositionAsync();

            GeoCoords result = new GeoCoords();
            result.Latitude = coordinates.Coordinate.Point.Position.Latitude;
            result.Longitude = coordinates.Coordinate.Point.Position.Longitude;

            return result;
        }
    }
}
