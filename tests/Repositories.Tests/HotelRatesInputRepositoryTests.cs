using HotelRates.Excel.Models;
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
        public void Should_Fetch_HoteRates_From_JSonFile()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var hotelRatesJsonFile = $@"{basePath}\HotelRates.json";
            HotelRatesInfoDto hotelRatesInfoDto = null; 

            using (var stream = new FileInfo(hotelRatesJsonFile).OpenRead())
            {
                IHotelRatesInputRepository repository = new HotelRatesInputRepository();
                hotelRatesInfoDto = repository.GetHotelRates(stream);
            }

            Assert.IsNotNull(hotelRatesInfoDto);
            Assert.IsTrue(hotelRatesInfoDto.HotelRates.Length > 0);
        }

        [Test]
        public void Should_Throw_Exception_When_Stream_Is_Empty()
        {
            IHotelRatesInputRepository repository = new HotelRatesInputRepository();
            Assert.Throws<ArgumentNullException>(() => repository.GetHotelRates(null));
        }

        [Test]
        public void Should_Throw_Exception_When_JSon_Invalid()
        {
            using (var stream = new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\Invalid.json").OpenRead())
            {
                IHotelRatesInputRepository repository = new HotelRatesInputRepository();
                var output = repository.GetHotelRates(stream);
                Assert.IsNull(output);
            }
        }
    }
}
