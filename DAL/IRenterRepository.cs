using HouseRental.Models;

namespace HouseRental.DAL
{
    public interface IRenterRepository
    {
        Task<IEnumerable<Renter>?> GetAll();
        Task<Renter?> GetRenterById(int id);
        Task<bool> Create(Renter renter);
        Task<bool> Update(Renter house);
        Task<bool> Delete(int id);

    }
}
