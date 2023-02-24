using Microsoft.Extensions.DependencyInjection;

namespace HotelRates.Excel.Services
{
    public class ConfigureServices
    {
        public void Configure(IServiceCollection services)
        {
            services.AddTransient(typeof(HotelRatesService));
            services.AddTransient(typeof(IHotelRatesService), typeof(HotelRatesService));
        }
    }
}
