using HouseRental.Models;

namespace HouseRental.ViewModel
{
    public class HouseListViewModel
    {
        public IEnumerable<House> Houses;               // en liste med hus som jeg skal vise i visningen 
        public string? CurrentViewName;

        public HouseListViewModel(IEnumerable<House> houses, string? currentViewName)
        {
            Houses = houses;
            CurrentViewName = currentViewName;
        }
    }
}
