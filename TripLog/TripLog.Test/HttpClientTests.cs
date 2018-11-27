namespace TripLog.Test
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TripLog.Services;

    [TestClass]
    public class HttpClientTests
    {
        private TripLogEntryDataServiceExtended _client;
        private Uri _url;

        [TestInitialize]
        public async Task Setup()
        {
            _url = new Uri("http://localhost:30080/api/TripLogWeb/");
            _client = new TripLogEntryDataServiceExtended(_url);
            await SetBaseLine();
        }

        [TestCleanup]
        public async Task ShutDown()
        {
            await SetBaseLine();
        }

        private async Task SetBaseLine()
        {
            if (_url != null)
            {
                await _client.RemoveAll();
            }
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