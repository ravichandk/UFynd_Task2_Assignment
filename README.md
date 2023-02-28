
# Hotel Rates Reporting

This repository contains the code that takes raw data in a JSON format as input and generates an excel by summarizing the hotel rates.

## Using the Sample Code

The code is completely independent and self-contained. The code can be analyzed to get an understanding of how a particular method works.

The project can be run from the command line.

## Running the samples from the Command Line
* Clone this repository:
```
    $ git clone https://github.com/ravichandk/UFynd_Task2_Assignment.git
```
 Open the solution in visual studio 2022.
 Build the project to produce the console app.

* Run the command for the excel to generate. For example:
```
     > main\Console\bin\Debug\net6.0\HotelRates.Excel.Console.exe main\Console\HotelRates.json
```
* Alternatively the project can be directly run from visual studio by pressing F5


# Automating the process
* We can .Net worker service approach to automate the process
* .Net worker service is a project template that contains features to run background services. These can be hosted as part of a Windows service.
* .Net worker service can be configured to run in every few intervals (minutes/hours) that can invoke the code to generate the excel.
* It can also be configured to monitor a folder where  JSON files with hote rates information can be dropped.
* The background service picks these files and generates corresponding excel sheets and send email.

* **Frameworks**
     * .Net 7
 
* **Libraries**
     * Microsoft.Extensions.Hosting
     * Microsoft.Extensions.Hosting.Windows
     * System.Net.Mail

Following is the code snippet that can be used to run the background service continously

```csharp
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
     while (!stoppingToken.IsCancellationRequested)
     {
          log.InfoFormat($"Worker running at: {DateTimeOffset.Now}");

          try
          {
               //Generates the excel file periodically
               await GenerateExcel();
          }
          catch (Exception ex)
          {
               log.ErrorFormat($"Exception while syncing files to S3. \n Ex: {ex}");
          }
          finally
          {
               //Interval time in which the background service will be paused
               await Task.Delay(AppSettings.MonitorIntervalInSeconds * 1000, stoppingToken);
          }
     }
}
```