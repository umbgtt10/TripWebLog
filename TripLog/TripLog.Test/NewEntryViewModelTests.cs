namespace TripLog.Test
{
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using TripLog.Models;
    using TripLog.Services;
    using TripLog.ViewModels;

    [TestClass]
    public class NewEntryViewModelTests
    {
        private NewEntryViewModel _vm;
        private Mock<ILocationService> _locMock;
        private Mock<ITripLogDataService> _dataMock;

        private GeoCoords _defaultGeoCoords;

        [TestInitialize]
        public void Setup()
        {
            _locMock = new Mock<ILocationService>();
            _dataMock = new Mock<ITripLogDataService>();
            _defaultGeoCoords = new GeoCoords()
            {
                Latitude = 123,
                Longitude = 321
            };
        }

        [TestMethod]
        public async Task InitEntryWithMockSetupIsSet()
        {
            _locMock.Setup(query => query.GetGeoCoordinatesAsync()).ReturnsAsync(_defaultGeoCoords);

            _vm = new NewEntryViewModel(_locMock.Object, _dataMock.Object);

            await _vm.Init();

            Assert.AreEqual(_defaultGeoCoords.Longitude, _vm.Longitude);
            Assert.AreEqual(_defaultGeoCoords.Latitude, _vm.Latitude);
        }
    }
}