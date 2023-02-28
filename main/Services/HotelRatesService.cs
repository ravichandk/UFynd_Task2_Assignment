using HotelRates.Excel.Repositories;
using log4net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("HotelRates.Excel.Services.Tests")]
namespace HotelRates.Excel.Services
{
    public interface IHotelRatesService
    {
        string GenerateExcel(Stream jsonFileStream);
    }

    internal class HotelRatesService : IHotelRatesService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HotelRatesService));

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
        string IHotelRatesService.GenerateExcel(Stream jsonFileStream)
        {
            string fileName = null;

            try
            {
                var hotelRatesInformation = _hotelRatesInputRepository.GetHotelRates(jsonFileStream);
                var hotelRates = new HotelRatesSummarizer().SummarizeHotelRates(hotelRatesInformation);
                fileName = _hotelRatesExcelRepository.GeneratExcel(hotelRates);
            }
            catch (Exception ex)
            {
                log.ErrorFormat($"Error while generating excel file from json data. \n Ex: {ex}");
            }

            return fileName;
        }
    }
}
