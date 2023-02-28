using HotelRates.Excel.Models;
using HotelRates.Excel.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace HotelRates.Excel.Repository.Tests
{
    [TestFixture]
    public class HotelRatesExcelRepositoryTests
    {
        [Test]
        public void Should_return_null_when_Input_Is_Null()
        {
            IHotelRatesExcelRepository hotelRatesExcelRepository = new HotelRatesExcelRepository();
            var excelFile = hotelRatesExcelRepository.GeneratExcel(null);

            Assert.IsNull(excelFile);
        }

        [Test]
        public void Should_return_null_when_Input_Is_Empty_List()
        {
            IHotelRatesExcelRepository hotelRatesExcelRepository = new HotelRatesExcelRepository();
            var excelFile = hotelRatesExcelRepository.GeneratExcel(new List<HotelRate>());

            Assert.IsNull(excelFile);
        }

        [Test]
        public void Should_return_null_when_Input_Is_A_Valid_List()
        {
            var tempDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}HotelRatesExcels";

            if (Directory.Exists(tempDirectory))
            {
                Directory.Delete(tempDirectory, true);
            }

            var hotelRates = new List<HotelRate>()
            {
                new HotelRate
                {
                    ArrivalDate = DateTime.UtcNow.AddDays(-1),
                    DepartureDate = DateTime.UtcNow.AddDays(1),
                    Adults = 1,
                    BreakFastIncluded = true,
                    Currency = "EUR",
                    Price = 100,
                    RateName = "Test"
                },
                new HotelRate
                {
                    ArrivalDate = DateTime.UtcNow.AddDays(-2),
                    DepartureDate = DateTime.UtcNow.AddDays(-1),
                    Adults = 1,
                    BreakFastIncluded = true,
                    Currency = "EUR",
                    Price = 90,
                    RateName = "Test"
                }
            };

            IHotelRatesExcelRepository hotelRatesExcelRepository = new HotelRatesExcelRepository();
            var excelFile = hotelRatesExcelRepository.GeneratExcel(hotelRates);

            Assert.IsNotNull(excelFile);
            File.Exists(excelFile);
        }
    }
}
