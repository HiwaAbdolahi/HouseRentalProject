using HouseRental.Models;

namespace HouseRental.ViewModel
{
    public class DetailsViewModel
    {
        public House House { get; set; }
        public bool IsHouse { get; set; }
        public bool IsOwner { get; set; }
        public bool IsRenter { get; set; }

        // Konstruktør som sikrer at House alltid får en verdi
        public DetailsViewModel(House house)
        {
            House = house;
        }
    }
}

