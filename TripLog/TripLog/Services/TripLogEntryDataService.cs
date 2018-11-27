namespace TripLog.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using TripLog.Models;

    public class TripLogEntryDataService: BaseHttpService, ITripLogDataService
    {
        protected readonly Uri _baseUri;
        protected readonly IDictionary<string, string> _headers;

        public TripLogEntryDataService(Uri baseUri)
        {
            _baseUri = baseUri;
            _headers = new Dictionary<string, string>();
        }

        public async Task<TripLogEntry> AddEntryAsync(TripLogEntry entry)
        { 
            var response = await SendRequestAsync<TripLogEntry>(_baseUri, HttpMethod.Post, _headers, entry);
            return response;
        }

        public async Task<IList<TripLogEntry>> GetEntriesAsync()
        {
            var response = await SendRequestAsync<TripLogEntry[]>(_baseUri, HttpMethod.Get, _headers);
            return response;
        }

        public async Task<TripLogEntry> GetEntryAsync(string id)
        {
            var url = new Uri(_baseUri, $"{id}");
            var response = await SendRequestAsync<TripLogEntry>(url, HttpMethod.Get, _headers);
            return response;
        }

        public async Task RemoveEntryAsync(TripLogEntry entry)
        {
            var url = new Uri(_baseUri, $"{entry.Id}");
            var response = await SendRequestAsync<TripLogEntry>(url, HttpMethod.Delete, _headers);
        }

        public async Task<TripLogEntry> UpdateEntryAsync(string id, TripLogEntry entry)
        {
            var url = new Uri(_baseUri, $"{id}");
            var response = await SendRequestAsync<TripLogEntry>(url, HttpMethod.Put, _headers, entry);
            return response;
        }
    }
}
