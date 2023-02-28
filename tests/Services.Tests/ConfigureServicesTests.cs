using HotelRates.Excel.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace HotelRates.Excel.Services.Tests
{
    [TestFixture]
    public class ConfigureServicesTests
    {
        [Test]
        public void Should_Build_Object_From_Service_Collection()
        {
            IServiceCollection services = new ServiceCollection();

            new ConfigureServices().Configure(services);
            new ConfigureRepositories().Configure(services);

            using (var provider = services.BuildServiceProvider())
            {
                var hotelRatesService = provider.GetService<IHotelRatesService>();
                Assert.IsNotNull(hotelRatesService);
            }
        }
    }
}
