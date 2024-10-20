using Microsoft.EntityFrameworkCore;
using HouseRental.Models;

namespace HouseRental.DAL
{
    public class HouseRepository : IHouseRepository
    {
        private readonly HouseDbContext _db;
        private readonly ILogger<HouseRepository> _logger;

        public HouseRepository(HouseDbContext db, ILogger<HouseRepository> logger)
        {

            _db = db;
            _logger = logger;
        }







        public async Task<IEnumerable<House>?> GetAll()
        {

            try
            {
                return  await _db.Houses
                .Include(h => h.Owner)
                .ToListAsync();
                

            }
            catch (Exception e) 
            {
                _logger.LogError("[HouseRepository] Houses TiListAync() failed when getAll(), error message: {e}", e.Message);
                return null;
            }
            

        }

        public async Task<House?> GetHouseById(int id)
        {
            try
            {
                return await _db.Houses.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("[HouseRepository] Houses FindAync(id) failed when getHouseById for HouseId {HouseId:0000}, error message: {e}", id, e.Message);
                return null;
            }
            
        }

        public async Task<bool> Create(House house)
        {

            try
            {
                _db.Houses.Add(house);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[HouseRepository] House Creation  failed for House {@house} error message: {e}", house, e.Message);
                return false;
            }
        }

        public async Task<bool> Update(House house)
        {
            try
            {
                _db.Houses.Update(house);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[HouseRepository] house FindeAsync(id) faild when updating the HouseId {HouseId: 0000},  error message: {e}", house, e.Message);
                return false;
            }
            
        }

        public async Task<bool> Delete(int id)
        {

            try 
            {
                var house = await _db.Houses.FindAsync(id);

                if (house == null)
                {
                    return false;
                }
                _db.Houses.Remove(house);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError("[HouseRepository] house Deletion Feiled for the HouseId {HouseId: 0000},  error message: {e}", id, e.Message);
                return false;
            }
           
        }


    }
}

