namespace TripLog.Services
{
    using System.Threading.Tasks;

    using TripLog.Models;

    public interface ILocationService
    {
        Task<GeoCoords> GetGeoCoordinatesAsync();
    }
}
