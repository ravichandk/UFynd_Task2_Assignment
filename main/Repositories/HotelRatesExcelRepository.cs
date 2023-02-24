using HotelRates.Excel.Models;

namespace HotelRates.Excel.Repositories
{
    public interface IHotelRatesExcelRepository
    {
        public void GeneratExcel(IList<HotelRate> hotelRates);
    }

    internal class HotelRatesExcelRepository : IHotelRatesExcelRepository
    {
        void IHotelRatesExcelRepository.GeneratExcel(IList<HotelRate> hotelRates)
        {
            throw new NotImplementedException();
        }
    }
}
