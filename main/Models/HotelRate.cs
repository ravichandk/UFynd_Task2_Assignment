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

        public string Price { get; set; }

        public string Currency { get; set; }

        public string RateName { get; set; }

        public int? Adults { get; set; }

        public bool BreakFastIncluded { get; set; }
    }
}
