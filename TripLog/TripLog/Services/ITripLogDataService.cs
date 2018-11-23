namespace TripLog.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TripLog.Models;

    public interface ITripLogDataService
    {
        Task<IList<TripLogEntry>> GetEntriesAsync();
        Task<TripLogEntry> GetEntryAsync(string id);
        Task<TripLogEntry> AddEntryAsync(TripLogEntry entry);
        Task<TripLogEntry> UpdateEntryAsync(string id, TripLogEntry entry);
        Task RemoveEntryAsync(TripLogEntry entry);
    }
}
