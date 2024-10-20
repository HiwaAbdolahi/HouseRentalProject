using Microsoft.EntityFrameworkCore;
using HouseRental.Models;


namespace HouseRental.DAL
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly HouseDbContext _db;
        private readonly ILogger<OwnerRepository> _logger;

        public OwnerRepository(HouseDbContext db, ILogger<OwnerRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<Owner>?> GetAll()
        {
            
           try
           {
               var owners = await _db.Owners.ToListAsync();
               return owners;
           }
            catch (Exception e)
            {
            _logger.LogError("[OwnerRepository] GetAll() failed with error: {e}", e.Message);
            return null;
            }
            


        }

        public async Task<Owner?> GetOwnerById(int id)
        {
            try
            {
                var owner = await _db.Owners.FindAsync(id);
                return owner;
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerRepository] GetOwnerById({OwnerId:0000}) failed with error: {e}", id, e.Message);
                return null;
            }
        }


        public async Task<bool> Create(Owner owner)
        {
            try
            {
                _db.Owners.Add(owner);
                await _db.SaveChangesAsync();
                return true; // Returner 'true' for å indikere vellykket opprettelse
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerRepository] Create failed with error: {e}", e.Message);
                return false; // Returner 'false' for å indikere at opprettelsen mislyktes
            }
        }


        public async Task<bool> Update(Owner owner)
        {
            try
            {
                _db.Owners.Update(owner);
                await _db.SaveChangesAsync();
                return true; // Returner 'true' for å indikere vellykket oppdatering
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerRepository] Update failed with error: {e}", e.Message);
                return false; // Returner 'false' for å indikere at oppdateringen mislyktes
            }
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                var owner = await _db.Owners.FindAsync(id);
                if (owner == null)
                {
                    return false;
                }

                _db.Owners.Remove(owner);
                await _db.SaveChangesAsync();
                return true; // Returner 'true' for å indikere vellykket sletting
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerRepository] Delete failed with error: {e}", e.Message);
                return false; // Returner 'false' for å indikere at slettingen mislyktes
            }
        }



    }
}
