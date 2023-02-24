using HotelRates.Excel.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace HotelRates.Excel.Repositories
{
    public interface IHotelRatesExcelRepository
    {
        public string GeneratExcel(IList<HotelRate> hotelRates);
    }

    internal class HotelRatesExcelRepository : IHotelRatesExcelRepository
    {
        string IHotelRatesExcelRepository.GeneratExcel(IList<HotelRate> hotelRates)
        {
            if (!Directory.Exists(@"C:\local")) Directory.CreateDirectory(@"C:\local");
            if (!Directory.Exists(@"C:\local")) Directory.CreateDirectory(@"C:\local\HotelRatesExcels");

            var fileName = $@"C:\local\HotelRatesExcels\output_{DateTime.UtcNow.Ticks}.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

                // setting the properties
                // of the work sheet 
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;

                // Setting the properties
                // of the first row
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;

                // Header of the Excel sheet
                workSheet.Cells[1, 1].Value = "ARRIVAL_DATE";
                workSheet.Cells[1, 2].Value = "DEPARTURE_DATE";
                workSheet.Cells[1, 3].Value = "PRICE";
                workSheet.Cells[1, 4].Value = "CURRENCY";
                workSheet.Cells[1, 5].Value = "RATENAME";
                workSheet.Cells[1, 6].Value = "ADULTS";
                workSheet.Cells[1, 7].Value = "BREAKFAST_INCLUDED";

                // Inserting the article data into excel
                // sheet by using the for each loop
                // As we have values to the first row 
                // we will start with second row
                int recordIndex = 2;

                foreach (var hotelRate in hotelRates)
                {
                    workSheet.Cells[recordIndex, 1].Value = hotelRate.ArrivalDate;
                    workSheet.Cells[recordIndex, 1].Style.Numberformat.Format = "yy.mm.dd";
                    workSheet.Cells[recordIndex, 2].Value = hotelRate.DepartureDate;
                    workSheet.Cells[recordIndex, 2].Style.Numberformat.Format = "yy.mm.dd";
                    workSheet.Cells[recordIndex, 3].Value = hotelRate.Price;
                    workSheet.Cells[recordIndex, 3].Style.Numberformat.Format = "0.00";
                    workSheet.Cells[recordIndex, 4].Value = hotelRate.Currency;
                    workSheet.Cells[recordIndex, 5].Value = hotelRate.RateName;
                    workSheet.Cells[recordIndex, 6].Value = hotelRate.Adults;
                    workSheet.Cells[recordIndex, 7].Value = hotelRate.BreakFastIncluded ? 1 : 0;

                    recordIndex++;
                }

                // By default, the column width is not set to auto fit for the content
                // of the range, so we are usingAutoFit() method here. 
                for (var i = 1; i <= 7; i++)
                    workSheet.Column(i).AutoFit();

                if (File.Exists(fileName))
                    File.Delete(fileName);

                // Create excel file on physical disk 
                FileStream objFileStrm = File.Create(fileName);
                objFileStrm.Close();

                // Write content to excel file 
                File.WriteAllBytes(fileName, excel.GetAsByteArray());
            }

            return fileName;
        }
    }
}
