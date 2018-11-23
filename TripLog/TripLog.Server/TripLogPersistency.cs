namespace TripLog.Server
{
    using System;
    using System.Collections.Generic;

    using TripLog.Models;

    public interface TripLogPersistency : IDisposable
    {
        IEnumerable<TripLogEntry> GetAll();
        TripLogEntry Get(string id);
        void Add(TripLogEntry value);
        void Remove(string id);
        void RemoveAll();
        void Update(string id, TripLogEntry value);
    }
}
