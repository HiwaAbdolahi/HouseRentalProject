using HouseRental.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRental.DAL
{
    public class LeaseAgreementRepository : ILeaseAgreementRepository
    {
        private readonly HouseDbContext _db;
        private readonly ILogger<ILeaseAgreementRepository> _logger;

        public LeaseAgreementRepository(HouseDbContext db, ILogger<ILeaseAgreementRepository> logger)
        {
            _db = db;
            _logger = logger;
        }





        public async Task<IEnumerable<LeaseAgreement>?> GetAll()
        {
            try
            {
                return await _db.LeaseAgreements.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementRepository] Error while fetching lease agreements. Error Message: {ex}", ex.Message);
                return null;
            }
        }





        public async Task<LeaseAgreement?> GetLeaseAgreementById(int id)
        {
            try
            {
                return await _db.LeaseAgreements.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementRepository] Error while fetching lease agreement by ID. Error Message: {ex}", ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateNewLeaseAgreement(LeaseAgreement leaseAgreement)
        {
            try
            {
                _db.LeaseAgreements.Add(leaseAgreement);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementRepository] Error while creating a new lease agreement. Error Message: {ex}", ex.Message);
                return false;
            }
        }



        public async Task<bool> Update(LeaseAgreement leaseAgreement)
        {
                try
                {
                    _db.LeaseAgreements.Update(leaseAgreement);
                    await _db.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("[LeaseAgreementRepository] Error while updating a lease agreement. Error Message: {ex}", ex.Message);
                    return false;
                }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var leaseAgreement = await _db.LeaseAgreements.FindAsync(id);
                if (leaseAgreement == null)
                {
                    return false;
                }

                _db.LeaseAgreements.Remove(leaseAgreement);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementRepository] Error while deleting a lease agreement. Error Message: {ex}", ex.Message);
                return false;
            }
        }
    }
}
