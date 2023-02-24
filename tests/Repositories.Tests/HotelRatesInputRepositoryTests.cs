using HotelRates.Excel.Repositories;
using NUnit.Framework;

namespace HotelRates.Excel.Repository.Tests
{
    [TestFixture]
    public class HotelRatesInputRepositoryTests
    {
        [Test]
        public void MustFetchHoteRatesFromJSonFile()
        {
            IHotelRatesInputRepository repository = new HotelRatesInputRepository();
            repository.GetHotelRates();
        }
    }
}
