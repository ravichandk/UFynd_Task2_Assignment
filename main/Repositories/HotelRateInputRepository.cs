using HotelRates.Excel.Models;
using log4net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("HotelRates.Excel.Repository.Tests")]
namespace HotelRates.Excel.Repositories
{
    /// <summary>
    /// Repository to fetch Hotel Rates.
    /// </summary>
    public interface IHotelRatesInputRepository
    {
        HotelRatesJsonDto GetHotelRates();
    }
    
    internal class HotelRatesInputRepository : IHotelRatesInputRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HotelRatesInputRepository));

        HotelRatesJsonDto IHotelRatesInputRepository.GetHotelRates()
        {
            log.InfoFormat("Fetching hotel rates");

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var hotelRatesJsonFile = $@"{basePath}\HotelRates.json";

            if(!File.Exists(hotelRatesJsonFile))
            {
                log.WarnFormat("No file exists for fetching Hotel Rates");
                return null;
            }

            HotelRatesJsonDto hotelRates = null;

            try
            {
                using (var content = new FileInfo(hotelRatesJsonFile).OpenRead())
                using (var streamReader = new StreamReader(content))
                {
                    hotelRates = Newtonsoft.Json.JsonConvert.DeserializeObject<HotelRatesJsonDto>(streamReader.ReadToEnd());
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
