using HotelRates.Excel.Repositories;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("HotelRates.Excel.Services.Tests")]
namespace HotelRates.Excel.Services
{
    public interface IHotelRatesService
    {
        string GenerateExcel(Stream stream);
    }

    internal class HotelRatesService : IHotelRatesService
    {
        private readonly IHotelRatesInputRepository _hotelRatesInputRepository;
        private readonly IHotelRatesExcelRepository _hotelRatesExcelRepository;

        public HotelRatesService(IHotelRatesInputRepository hotelRatesInputRepository,
            IHotelRatesExcelRepository hotelRatesExcelRepository)
        {
            _hotelRatesInputRepository = hotelRatesInputRepository ?? throw new ArgumentNullException(nameof(hotelRatesInputRepository));
            _hotelRatesExcelRepository = hotelRatesExcelRepository ?? throw new ArgumentNullException(nameof(hotelRatesExcelRepository));
        }

        /// <summary>
        /// Fetches data from JSON file and summarizes them into flattened data
        /// that can be written in an excel format
        /// </summary>
        string IHotelRatesService.GenerateExcel(Stream stream)
        {
            var hotelRatesInformation = _hotelRatesInputRepository.GetHotelRates(stream);
            var hotelRates = new HotelRatesSummarizer().SummarizeHotelRates(hotelRatesInformation);
            return _hotelRatesExcelRepository.GeneratExcel(hotelRates);
        }
    }
}
