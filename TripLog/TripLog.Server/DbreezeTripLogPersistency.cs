namespace TripLog.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using TripLog.Models;

    using DBreeze;

    public class DbreezeTripLogPersistency : TripLogPersistency, TripLogPersistencyInitializable, IDisposable
    {
        private string _tableName = "TripLogTable";
        private DBreezeEngine _db;
        protected DirectoryInfo _dbDirectory;

        public DbreezeTripLogPersistency(DirectoryInfo directory)
        {
            _dbDirectory = directory;
            _db = new DBreezeEngine(directory.FullName);
        }

        public void Setup()
        {
            if (!_dbDirectory.Exists)
            {
                _dbDirectory.Create();
            }            
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }

        public void Add(TripLogEntry value)
        {
            using (var transaction = _db.GetTransaction())
            {
                transaction.Insert(_tableName, value.Id, TripLogEntry.Serialize(value));
                transaction.Commit();
            }
        }

        public TripLogEntry Get(string id)
        {
            TripLogEntry result;

            using (var transaction = _db.GetTransaction())
            {
                var select = transaction.Select<string, string>(_tableName, id);
                result = select.Exists ? TripLogEntry.Deserialize(select.Value) : null;
                transaction.Commit();
            }

            return result;
        }

        public IEnumerable<TripLogEntry> GetAll()
        {
            IList<TripLogEntry> result;

            using (var transaction = _db.GetTransaction())
            {
                var select = transaction.SelectForward<string, string>(_tableName);
                result = select.Select(elem => TripLogEntry.Deserialize(elem.Value)).ToList();
                transaction.Commit();
            }

            return result;
        }

        public void Remove(string id)
        {
            using (var transaction = _db.GetTransaction())
            {
                transaction.RemoveKey(_tableName, id);
                transaction.Commit();
            }
        }

        public void RemoveAll()
        {
            using (var transaction = _db.GetTransaction())
            {
                transaction.RemoveAllKeys(_tableName, true);
                transaction.Commit();
            }
        }

        public void Update(string id, TripLogEntry value)
        {
            using (var transaction = _db.GetTransaction())
            {
                transaction.RemoveKey(_tableName, id);
                transaction.Insert(_tableName, value.Id, TripLogEntry.Serialize(value));
                transaction.Commit();
            }
        }
    }
}
