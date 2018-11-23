namespace TripLog.Test
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TripLog.Services;

    [TestClass]
    public class HttpClientTests
    {
        private ITripLogDataService _client;
        private Uri _url;

        [TestInitialize]
        public void Setup()
        {
            _url = new Uri("http://192.168.1.21:30080/api/TripLogWeb/");
            _client = new TripLogEntryDataService(_url);
        }

        [TestMethod]
        public async Task SyncSimpleInsertRetrieve()
        {
            await _client.AddEntryAsync(Common.First);

            var retrieved = await _client.GetEntryAsync(Common.First.Id);

            Assert.AreEqual(Common.First, retrieved);

            await _client.RemoveEntryAsync(Common.First);
        }

        [TestMethod]
        public async Task SyncSimpleInsertRemove()
        {
            await _client.AddEntryAsync(Common.First);

            await _client.RemoveEntryAsync(Common.First);

            var result = await _client.GetEntriesAsync();

            Assert.AreEqual(0, result.Count);            
        }
        [TestMethod]
        public async Task SyncSimpleInsertUpdate()
        {
            await _client.AddEntryAsync(Common.First);

            await _client.UpdateEntryAsync(Common.First.Id, Common.Second);

            var result = await _client.GetEntriesAsync();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(result[0], Common.Second);

            await _client.RemoveEntryAsync(Common.Second);
        }
    }
}