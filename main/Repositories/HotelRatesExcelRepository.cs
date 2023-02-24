using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using HotelRates.Excel.Models;

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

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                Row headerRow = new Row();

                headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue("ARRIVAL_DATE") });
                headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue("DEPARTURE_DATE") });
                headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue("PRICE") });
                headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue("CURRENCY") });
                headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue("RATENAME") });
                headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue("ADULTS") });
                headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue("BREAKFAT_INCLUDED") });

                sheetData.AppendChild(headerRow);

                foreach (var hotelRate in hotelRates)
                {
                    var newRow = new Row();

                    newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(hotelRate.ArrivalDate?.ToShortDateString()) });
                    newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(hotelRate.DepartureDate?.ToShortDateString()) });
                    newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(hotelRate.Price?.ToString()) });
                    newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(hotelRate.Currency)});
                    newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(hotelRate.RateName)});
                    newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(hotelRate.Adults?.ToString())});
                    newRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(hotelRate.BreakFastIncluded ? "1" : "0") });

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }

            return fileName;
        }
    }
}
