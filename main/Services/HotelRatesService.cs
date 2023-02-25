using HotelRates.Excel.Models;
using HotelRates.Excel.Repositories;

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
            var hotelRates = HotelRatesSummarizer.SummarizeHotelRates(hotelRatesInformation);
            return _hotelRatesExcelRepository.GeneratExcel(hotelRates);
        }
    }

    internal class HotelRatesSummarizer
    {
        public static IList<HotelRate> SummarizeHotelRates(HotelRatesInfoDto hotelRatesInfoDto)
        {
            var hotelRates = 
                hotelRatesInfoDto?.HotelRates?
                .Select(h => new HotelRate
                {
                    ArrivalDate = h.TargetDay,
                    DepartureDate = h.TargetDay?.AddDays(h.Los),
                    Price = h.Price?.NumericFloat,
                    Currency = h.Price?.Currency,
                    RateName = h.RateName,
                    Adults = h.Adults,
                    BreakFastIncluded = h.RateTags?.FirstOrDefault(r => r.Name?.Equals("breakfast", StringComparison.OrdinalIgnoreCase) ?? false) != null
                }).ToList();

            return hotelRates;
        }
    }
}
