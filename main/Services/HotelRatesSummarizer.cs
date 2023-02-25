using HotelRates.Excel.Models;

namespace HotelRates.Excel.Services
{
    internal class HotelRatesSummarizer
    {
        public virtual IList<HotelRate> SummarizeHotelRates(HotelRatesInfoDto hotelRatesInfoDto)
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
