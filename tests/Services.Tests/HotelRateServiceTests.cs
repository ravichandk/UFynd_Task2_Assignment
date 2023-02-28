using HotelRates.Excel.Models;
using HotelRates.Excel.Repositories;
using Moq;
using NUnit.Framework;

namespace HotelRates.Excel.Services.Tests
{
    [TestFixture]
    public class HotelRateServiceTests
    {
        [Test]
        public void Should_Throw_Error_When_Stream_Is_Empty()
        {
            var hotelRatesInputRepository = new Mock<IHotelRatesInputRepository>();
            var hotelRatesExcelRepository = new Mock<IHotelRatesExcelRepository>();

            Assert.Throws<ArgumentNullException>(() => new HotelRatesService(null, hotelRatesExcelRepository.Object));
            Assert.Throws<ArgumentNullException>(() => new HotelRatesService(hotelRatesInputRepository.Object, null));
            Assert.Throws<ArgumentNullException>(() => new HotelRatesService(null, null));

            hotelRatesInputRepository.Setup(x => x.GetHotelRates(null)).Throws<ArgumentNullException>();

            IHotelRatesService hoteRatesService = new HotelRatesService(hotelRatesInputRepository.Object, hotelRatesExcelRepository.Object);
            var output = hoteRatesService.GenerateExcel(null);
            Assert.IsNull(output);
        }

        [Test]
        public void Should_Return_Empty_File_When_HotelRates_Is_Empty()
        {
            var hotelRatesInputRepository = new Mock<IHotelRatesInputRepository>();
            var hotelRatesExcelRepository = new Mock<IHotelRatesExcelRepository>();

            hotelRatesInputRepository.Setup(x => x.GetHotelRates(null)).Returns(() => null);
            hotelRatesExcelRepository.Setup(x => x.GeneratExcel(null)).Returns(() => null);

            IHotelRatesService hoteRatesService = new HotelRatesService(hotelRatesInputRepository.Object, hotelRatesExcelRepository.Object);
            var result = hoteRatesService.GenerateExcel(null);

            Assert.IsNull(result);
        }

        [Test]
        public void Should_Return_Empty_File_When_HotelRates_Is_EmptyList()
        {
            var hotelRatesInputRepository = new Mock<IHotelRatesInputRepository>();
            var hotelRatesExcelRepository = new Mock<IHotelRatesExcelRepository>();

            hotelRatesInputRepository.Setup(x => x.GetHotelRates(null)).Returns(() => new HotelRatesInfoDto { HotelRates = new List<HotelRateDto>().ToArray()});
            hotelRatesExcelRepository.Setup(x => x.GeneratExcel(new List<HotelRate>())).Returns(() => null);

            IHotelRatesService hoteRatesService = new HotelRatesService(hotelRatesInputRepository.Object, hotelRatesExcelRepository.Object);
            var result = hoteRatesService.GenerateExcel(null);

            Assert.IsNull(result);
        }

        [Test]
        public void Should_Return_Valid_File_When_HotelRates_Is_A_ValidList()
        {
            IHotelRatesService hotelRatesService = new HotelRatesService(new HotelRatesInputRepository(), new HotelRatesExcelRepository());
            string excelGenerated = null;

            using (var stream = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}/HotelRates.json").OpenRead())
            {
                excelGenerated = hotelRatesService.GenerateExcel(stream);
            }

            Assert.IsNotNull(excelGenerated);
            Assert.IsTrue(File.Exists(excelGenerated));
        }
    }
}
