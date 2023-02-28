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

        //The file [Full file path] that contains JSON content will taken as an input
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

            using (var jsonFileStream = new FileInfo(args[0]).OpenRead())
            {
                excelGenerated = hotelRatesService.GenerateExcel(jsonFileStream);

                Process.Start("explorer", excelGenerated);
            }
        }

        //Reduce the storage automatically
        CleanOldFiles(excelGenerated, f => f.CreationTimeUtc <= DateTime.UtcNow.AddDays(-2));
    }

    private static void CleanOldFiles(string currentFileGenerated, Func<FileInfo, bool> predicate)
    {
        try
        {
            //Deleting files that are older than 2 days.
            //This can be custommizable based on the data generated
            Directory
                .GetFiles(Path.GetDirectoryName(currentFileGenerated)).ToList()
                .Select(f => new FileInfo(f)).ToList()
                .Where(predicate).ToList()
                .ForEach(file => file.Delete());
        }
        catch (Exception ex)
        {
            //As this is an do good operation, it should not impact the main process.
        }
    }
}


