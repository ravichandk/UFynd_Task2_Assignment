namespace HotelRates.Excel.Models
{
    /// <summary>
    /// Class that is used to deserialize the Hotel Rates JSON into a model 
    /// </summary>
    public class HotelRatesJsonDto
    {
        public HotelInfoDto Hotel { get; set; }

        public HotelRateDto[] HotelRates { get; set; }
    }

    public class HotelInfoDto
    {
        public int HotelId { get; set; }

        public int Classification { get; set; }

        public string Name { get; set; }

        public double ReviewScore { get; set; }
    }

    public class HotelRateDto
    {
        public int Adults { get; set; }

        public int Los { get; set; }

        public Price Price { get; set; }

        public string RateDescription { get; set; }

        public string RateId { get; set; }

        public string RateName { get; set; }

        public RateTag[] RateTags { get; set; }

        public DateTime? TargetDay { get; set; }
    }

    public class Price
    {
        public string Currency { get; set; }

        public double NumericFloat { get; set; }

        public int NumericInteger { get; set; }
    }

    public class RateTag
    {
        public string Name { get; set; }

        public bool Shape { get; set; }
    }
}
