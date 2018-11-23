namespace TripLog.Server.Test
{    
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TripLog.Models;

    [TestClass]
    public class PersistencyTests
    {
        private string _dbSubFolder = "DbTemp";
        private ExtendedDbreezeTripLogPersistency _db;
        private DirectoryInfo _dbFolder;

        [TestInitialize]
        public void Setup()
        {
            _dbFolder = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), _dbSubFolder));
            _db = new ExtendedDbreezeTripLogPersistency(_dbFolder);
            _db.Setup();
        }

        [TestCleanup]
        public void ShutDown()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db.ShutDown();
            }
        }

        [TestMethod]
        public void TestAddElementToPersistency()
        {
            var newTripLogEntry = new TripLogEntry();
            newTripLogEntry.Id = "First";

            _db.Add(newTripLogEntry);

            var dump = _db.GetAll();

            Assert.AreEqual(1, dump.Count());
            Assert.AreEqual(newTripLogEntry, dump.First());
        }

        [TestMethod]
        public void TestRemoveElementFromPersistency()
        {
            var newTripLogEntry1 = new TripLogEntry();
            newTripLogEntry1.Id = "First";
            _db.Add(newTripLogEntry1);

            var newTripLogEntry2 = new TripLogEntry();
            newTripLogEntry2.Id = "Second";
            _db.Add(newTripLogEntry2);

            _db.Remove(newTripLogEntry1.Id);

            var dump = _db.GetAll();

            Assert.AreEqual(1, dump.Count());
            Assert.AreEqual(newTripLogEntry2, dump.First());
        }

        [TestMethod]
        public void TestRemoveInexistentElementFromPersistency()
        {
            var newTripLogEntry1 = new TripLogEntry();
            newTripLogEntry1.Id = "First";
            _db.Add(newTripLogEntry1);

            _db.Remove("Inexistent!");

            var dump = _db.GetAll();

            Assert.AreEqual(1, dump.Count());
            Assert.AreEqual(newTripLogEntry1, dump.First());
        }

        [TestMethod]
        public void TestGetElementFromPersistency()
        {
            var newTripLogEntry1 = new TripLogEntry();
            newTripLogEntry1.Id = "First";
            _db.Add(newTripLogEntry1);

            var newTripLogEntry2 = new TripLogEntry();
            newTripLogEntry2.Id = "Second";
            _db.Add(newTripLogEntry2);

            var element = _db.Get(newTripLogEntry1.Id);

            Assert.AreEqual(newTripLogEntry1, element);
        }

        [TestMethod]
        public void TestUpdateElementOnPersistency()
        {
            var newTripLogEntry1 = new TripLogEntry();
            newTripLogEntry1.Id = "First";
            _db.Add(newTripLogEntry1);

            newTripLogEntry1.Rating = 100;
            _db.Update(newTripLogEntry1.Id, newTripLogEntry1);
            var element = _db.Get(newTripLogEntry1.Id);

            Assert.AreEqual(newTripLogEntry1.Rating, element.Rating);
        }

        [TestMethod]
        public void TestRemoveEveryElementFromPersistency()
        {
            var newTripLogEntry1 = new TripLogEntry();
            newTripLogEntry1.Id = "First";
            _db.Add(newTripLogEntry1);

            var newTripLogEntry2 = new TripLogEntry();
            newTripLogEntry2.Id = "Second";
            _db.Add(newTripLogEntry2);

            _db.RemoveAll();

            var dump = _db.GetAll();

            Assert.AreEqual(0, dump.Count());
        }
    }
}
