using HotelRates.Excel.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace HotelRates.Excel.Repository.Tests
{
    [TestFixture]
    public class ConfigureRepositoriesTests
    {
        [Test]
        public void Should_Build_Object_From_Service_Collection()
        {
            IServiceCollection services = new ServiceCollection();

            new ConfigureRepositories().Configure(services);

            using (var provider = services.BuildServiceProvider())
            {
                var hotelRatesInputRepository = provider.GetService<IHotelRatesInputRepository>();
                Assert.IsNotNull(hotelRatesInputRepository);

                var hotelRatesExcelRepository = provider.GetService<IHotelRatesExcelRepository>();
                Assert.IsNotNull(hotelRatesExcelRepository);
            }
        }
    }
}
