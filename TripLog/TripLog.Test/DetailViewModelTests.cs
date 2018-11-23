namespace TripLog.Test
{
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using TripLog.Models;
    using TripLog.ViewModels;

    [TestClass]
    public class DetailViewModelTests
    {
        private DetailViewModel _vm;

        [TestInitialize]
        public void Setup()
        {
            _vm = new DetailViewModel();
        }

        [TestMethod]
        public async Task InitParameterProvidedEntryIsSet()
        {
            var mockEntry = new Mock<TripLogEntry>().Object;
            _vm.Entry = null;

            await _vm.Init(mockEntry);

            Assert.IsNotNull(_vm.Entry);
        }

        [TestMethod]
        public async Task InitParameterNotProvidedThrows()
        {
            bool isExceptionThrown = false;

            try
            {
                await _vm.Init();
            }
            catch (EntryNotProvidedException e)
            {
                isExceptionThrown = true;
            }

            Assert.IsTrue(isExceptionThrown);
        }
    }
}