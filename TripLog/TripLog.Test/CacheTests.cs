namespace TripLog.Test
{
    using System;
    using System.Threading.Tasks;
    using System.Reactive.Linq;
    using System.Threading;

    using Akavache;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TripLog.Models;

    [TestClass]
    public class CacheTests
    {
        private IBlobCache _cache;
        private string _tripLogCacheName = "TripLog";
        private TripLogEntry _updatedSecond = new TripLogEntry("Second", 3);
        private TripLogEntry _result;

        [TestInitialize]
        public void Setup()
        {
            BlobCache.ApplicationName = "TripLog";
            BlobCache.EnsureInitialized();
            _cache = BlobCache.LocalMachine;
        }

        [TestMethod]
        public async Task SyncSimpleInsertRetrieve()
        {
            await _cache.InsertObject(_tripLogCacheName, Common.First);

            TripLogEntry result = await _cache.GetObject<TripLogEntry>(_tripLogCacheName);

            Assert.AreEqual(Common.First.Id, result.Id);
        }

        [TestMethod]
        public async Task SyncSimpleInsertReplaceRetrieve()
        {
            await _cache.InsertObject(_tripLogCacheName, Common.First);
            await _cache.InsertObject(_tripLogCacheName, Common.Second);

            TripLogEntry result = await _cache.GetObject<TripLogEntry>(_tripLogCacheName);

            Assert.AreEqual(Common.Second.Id, result.Id);
        }

        [TestMethod]
        public async Task AsyncSimpleInsertRetrieve()
        {
            await _cache.InsertObject(_tripLogCacheName, Common.First);

            TripLogEntry result = new TripLogEntry();

            _cache.GetObject<TripLogEntry>(_tripLogCacheName).Subscribe(x => result = x);

            Thread.Sleep(1000);

            Assert.AreEqual(Common.First.Id, result.Id);
        }

        [TestMethod]
        public async Task AsyncSimpleReplaceInsertRetrieve()
        {
            await _cache.InsertObject(_tripLogCacheName, Common.First);
            await _cache.InsertObject(_tripLogCacheName, Common.Second);

            TripLogEntry result = new TripLogEntry();

            _cache.GetObject<TripLogEntry>(_tripLogCacheName).Subscribe(x => result = x);

            Thread.Sleep(1000);

            Assert.AreEqual(Common.Second.Id, result.Id);
        }

        [TestMethod]
        public async Task AsyncCachedInsertRetrieve()
        {
            await _cache.InsertObject(_tripLogCacheName, Common.Second);

            _cache.GetAndFetchLatest(_tripLogCacheName, PullEntriesFromService()).Subscribe(Observer);

            Thread.Sleep(1000);

            Assert.AreEqual(_updatedSecond.Id, _result.Id);
            Assert.AreEqual(_updatedSecond.Rating, _result.Rating);
        }

        private Func<Task<TripLogEntry>> PullEntriesFromService()
        {
            var task = new Task<TripLogEntry>(ReturnUpdatedSecond);

            task.Start();

            return async () => await task;
        }

        private TripLogEntry ReturnUpdatedSecond()
        {
            return _updatedSecond;
        }

        private void Observer(TripLogEntry onNext)
        {
            _result = onNext;
        }
    }
}