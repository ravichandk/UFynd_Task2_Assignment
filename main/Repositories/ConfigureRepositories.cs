using Microsoft.Extensions.DependencyInjection;

namespace HotelRates.Excel.Repositories
{
    public class ConfigureRepositories
    {
        public void Configure(IServiceCollection services)
        {
            services.AddTransient(typeof(HotelRatesInputRepository));
            services.AddTransient(typeof(IHotelRatesInputRepository), typeof(HotelRatesInputRepository));
            services.AddTransient(typeof(HotelRatesExcelRepository));
            services.AddTransient(typeof(IHotelRatesExcelRepository), typeof(HotelRatesExcelRepository));
        }
    }
}
