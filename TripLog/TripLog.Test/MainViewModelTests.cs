namespace TripLog.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Akavache;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using TripLog.Models;
    using TripLog.Services;
    using TripLog.ViewModels;

    [TestClass]
    public class MainViewModelTests
    {
        private MainViewModel _vm;
        private Mock<ITripLogDataService> _dataMock;
        private IBlobCache _cache;

        private IList<TripLogEntry> _entriesList;

        [TestInitialize]
        public void Setup()
        {
            _dataMock = new Mock<ITripLogDataService>();
            BlobCache.ApplicationName = "TripLogWeb";
            BlobCache.EnsureInitialized();
            _cache = BlobCache.LocalMachine;
            _entriesList = new List<TripLogEntry>() { Common.First };
        }

        [TestMethod]
        public async Task InitEntryWithMockSetupIsSet()
        {
            _dataMock.Setup(query => query.GetEntriesAsync()).ReturnsAsync(_entriesList);

            _vm = new MainViewModel(_dataMock.Object, _cache);

            await _vm.Init();

            EnforceSynchonization();

            Assert.AreEqual(1, _vm.LogEntries.Count);
            Assert.AreEqual(_vm.LogEntries.First(), _entriesList.First());
        }

        private static void EnforceSynchonization()
        {
            Thread.Sleep(1000);
        }
    }
}