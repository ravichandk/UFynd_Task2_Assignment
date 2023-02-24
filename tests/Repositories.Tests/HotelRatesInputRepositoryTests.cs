using HotelRates.Excel.Repositories;
using NUnit.Framework;
using System;
using System.IO;

namespace HotelRates.Excel.Repository.Tests
{
    [TestFixture]
    public class HotelRatesInputRepositoryTests
    {
        [Test]
        public void MustFetchHoteRatesFromJSonFile()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var hotelRatesJsonFile = $@"{basePath}\HotelRates.json";

            using (var stream = new FileInfo(hotelRatesJsonFile).OpenRead())
            {
                IHotelRatesInputRepository repository = new HotelRatesInputRepository();
                repository.GetHotelRates(stream);
            }
        }
    }
}
