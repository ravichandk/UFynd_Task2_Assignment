// See https://aka.ms/new-console-template for more information
using HotelRates.Excel.Repositories;
using HotelRates.Excel.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

class TestClass
{
    static void Main(string[] args)
    {
        //For testing purpose, the argument has been set to "Test".
        //The program will use the HotelRates.json in the project
        if (args.Length == 1 && args[0] == "Test")
        {
            args[0] = $"{AppDomain.CurrentDomain.BaseDirectory}/HotelRates.json";
        }

        if (args.Length <= 0)
        {
            Console.WriteLine("File Name should be mentioned as the first argument");
            return;
        }

        if (!File.Exists(args[0]))
        {
            Console.WriteLine("The first argument should be json file name");
            return;
        }

        var services = new ServiceCollection();

        new ConfigureServices().Configure(services);
        new ConfigureRepositories().Configure(services);

        string excelGenerated;
        using (var provider = services.BuildServiceProvider())
        {
            var hotelRatesService = provider.GetService<IHotelRatesService>();

            using (var stream = new FileInfo(args[0]).OpenRead())
            {
                excelGenerated = hotelRatesService.GenerateExcel(stream);

                Process.Start("explorer", excelGenerated);
            }
        }

        CleanOldFiles(excelGenerated);
    }

    private static void CleanOldFiles(string excelGenerated)
    {
        Directory
                    .GetFiles(Path.GetDirectoryName(excelGenerated)).ToList()
                    .Except(new[] { excelGenerated }).ToList()
                    .ForEach(file => File.Delete(file));
    }
}


