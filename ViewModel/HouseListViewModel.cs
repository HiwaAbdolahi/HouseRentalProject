using HouseRental.Models;

namespace HouseRental.ViewModel
{
    public class HouseListViewModel
    {
        public IEnumerable<House> Houses;
        public string? CurrentViewName;

        public HouseListViewModel(IEnumerable<House> houses, string? currentViewName)
        {
            Houses = houses;
            CurrentViewName = currentViewName;
        }
    }
}
