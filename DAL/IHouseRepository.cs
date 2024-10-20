using HouseRental.Models;

namespace HouseRental.DAL
{
    public interface IHouseRepository
    {
        Task<IEnumerable<House>?> GetAll();
        Task<House?> GetHouseById(int id);
        Task<bool> Create (House house);
        Task<bool> Update (House house);
        Task<bool> Delete (int id);

    }
}
