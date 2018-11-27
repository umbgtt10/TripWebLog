namespace TripLog.Test
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TripLog.Models;
    using TripLog.Services;

    public class TripLogEntryDataServiceExtended : TripLogEntryDataService
    {
        public TripLogEntryDataServiceExtended(Uri baseUri) : base(baseUri)
        {
        }

        public async Task RemoveAll()
        {
            var response = await SendRequestAsync<TripLogEntry>(_baseUri, HttpMethod.Delete, _headers);
        }
    
    }
}