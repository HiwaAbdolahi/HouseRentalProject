using HouseRental.Models;

namespace HouseRental.DAL
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>?> GetAll();
        
        Task<Owner?> GetOwnerById(int id);   //addet ?

        Task<bool> Create(Owner owner);

        Task<bool> Update(Owner owner);

        Task<bool> Delete(int id);
    }
}
