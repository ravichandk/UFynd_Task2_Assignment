namespace HotelRates.Excel.Models
{
    /// <summary>
    /// Model class that defines the structure of Hotel Rates
    /// This is used to generate the excel sheet that reports the data
    /// </summary>
    public class HotelRate
    {
        public DateTime? ArrivalDate { get; set; }

        public DateTime? DepartureDate { get; set; }

        public double? Price { get; set; }

        public string Currency { get; set; }
    }
}
