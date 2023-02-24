// See https://aka.ms/new-console-template for more information
using HotelRates.Excel.Repositories;
using HotelRates.Excel.Services;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

new ConfigureServices().Configure(services);
new ConfigureRepositories().Configure(services);

using (var provider = services.BuildServiceProvider())
{
    var hotelRatesService = provider.GetService<IHotelRatesService>();

    using (var stream = new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\HotelRates.json").OpenRead())
        hotelRatesService.GenerateExcel(stream);
}


