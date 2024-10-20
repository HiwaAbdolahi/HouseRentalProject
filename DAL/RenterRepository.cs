using HouseRental.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRental.DAL
{
    public class RenterRepository : IRenterRepository
    {
        private readonly HouseDbContext _db;
        private readonly ILogger<IRenterRepository> _logger;

        public RenterRepository(HouseDbContext db, ILogger<IRenterRepository> logger)
        {
            _db = db;
            _logger = logger;
        }





        public async Task<IEnumerable<Renter>?> GetAll()
        {
            try
            {
                return await _db.Renters.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("[RenterRepository] An error occurred while fetching renters: {e}", e.Message);
                return null;
            }
        }





        public async Task<Renter?> GetRenterById(int id)
        {
            try
            {
                return await _db.Renters.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("[RenterRepository] An error occurred while fetching renter by ID {RenterId:0000}: {e}", id, e.Message);
                return null;
            }
        }

        public async Task<bool> Create(Renter renter)
        {
            try
            {
                _db.Renters.Add(renter);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[RenterRepository] An error occurred while creating renter: {e}", e.Message);
                return false;
            }
        }

        public async Task<bool> Update(Renter renter)
        {
            try
            {
                _db.Renters.Update(renter);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[RenterRepository] An error occurred while updating renter: {e}", e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var renter = await _db.Renters.FindAsync(id);
                if (renter == null)
                {
                    return false;
                }

                _db.Renters.Remove(renter);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[RenterRepository] An error occurred while deleting renter by ID {RenterId:0000}: {e}", id, e.Message);
                return false;
            }
        }

    }
}
