using HotelRates.Excel.Models;
using log4net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("HotelRates.Excel.Repository.Tests")]
[assembly: InternalsVisibleToAttribute("HotelRates.Excel.Services.Tests")]
namespace HotelRates.Excel.Repositories
{
    /// <summary>
    /// Repository to fetch Hotel Rates.
    /// </summary>
    public interface IHotelRatesInputRepository
    {
        HotelRatesInfoDto GetHotelRates(Stream stream);
    }
    
    internal class HotelRatesInputRepository : IHotelRatesInputRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HotelRatesInputRepository));

        HotelRatesInfoDto IHotelRatesInputRepository.GetHotelRates(Stream stream)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            log.InfoFormat("Fetching hotel rates");

            HotelRatesInfoDto hotelRates = null;

            try
            {
                using (var streamReader = new StreamReader(stream))
                {
                    hotelRates = Newtonsoft.Json.JsonConvert.DeserializeObject<HotelRatesInfoDto>(streamReader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat($"Exception occurred while serializing data from JSON file. \n Ex: {ex}");
            }

            return hotelRates;
        }
    }
}
