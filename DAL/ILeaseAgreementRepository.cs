using HouseRental.Models;

namespace HouseRental.DAL
{
    public interface ILeaseAgreementRepository
    {
        Task<IEnumerable<LeaseAgreement>?> GetAll();
        Task<LeaseAgreement?> GetLeaseAgreementById(int id);
        Task<bool> CreateNewLeaseAgreement(LeaseAgreement leaseAgreement);
        Task<bool> Update(LeaseAgreement leaseAgreement);
        Task<bool> Delete(int id);
        
    }
}
