// See https://aka.ms/new-console-template for more information
using HotelRates.Excel.Repositories;
using HotelRates.Excel.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

class TestClass
{
    static void Main(string[] args)
    {
        if(args.Length <= 0)
        {
            Console.WriteLine("File Name should be mentioned as the first argument");
            return;
        }

        if(!File.Exists(args[0]))
        {
            Console.WriteLine("The first argument should be json file name");
            return;
        }

        var services = new ServiceCollection();

        new ConfigureServices().Configure(services);
        new ConfigureRepositories().Configure(services);

        using (var provider = services.BuildServiceProvider())
        {
            var hotelRatesService = provider.GetService<IHotelRatesService>();

            using (var stream = new FileInfo(args[0]).OpenRead())
            {
                var excelGenerated = hotelRatesService.GenerateExcel(stream);

                Process.Start("explorer", excelGenerated);
            }
        }
    }
}




