namespace TripLog.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Http;

    using TripLog.Models;

    public class TripLogWebController : ApiController, IDisposable
    {
        private TripLogPersistency _tripLogPersistency;

        public TripLogWebController()
        {
            var db = new DbreezeTripLogPersistency(new DirectoryInfo(@"D:\TripLogPersistency"));
            db.Setup();
            _tripLogPersistency = db;
        }

        // GET api/TripLogWeb
        public IEnumerable<TripLogEntry> Get()
        {
            var result = _tripLogPersistency.GetAll();

            _tripLogPersistency.Dispose();

            return result;
        }

        // GET api/TripLogWeb/5
        public TripLogEntry Get(string id)
        {
            var result = _tripLogPersistency.Get(id);

            _tripLogPersistency.Dispose();

            return result;
        }

        // POST api/TripLogWeb
        public void Post([FromBody]TripLogEntry value)
        {
            _tripLogPersistency.Add(value);

            _tripLogPersistency.Dispose();
        }

        // PUT api/TripLogWeb/5
        public void Put(string id, [FromBody]TripLogEntry value)
        {
            _tripLogPersistency.Update(id, value);

            _tripLogPersistency.Dispose();
        }

        // DELETE api/TripLogWeb/5
        public void Delete(string id)
        {
            _tripLogPersistency.Remove(id);

            _tripLogPersistency.Dispose();
        }

        // DELETE api/TripLogWeb/
        public void Delete()
        {
            _tripLogPersistency.RemoveAll();

            _tripLogPersistency.Dispose();
        }
    }
}
