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














        public async Task<bool> AddHouseImages(int houseId, List<string> imageUrls)
        {
            try
            {
                var house = await _db.Houses.FindAsync(houseId);
                if (house == null) return false;

                var houseImages = imageUrls.Select(url => new HouseImage
                {
                    HouseId = houseId,
                    ImageUrl = url
                }).ToList();

                await _db.HouseImages.AddRangeAsync(houseImages);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[HouseRepository] Failed to add images for HouseId {HouseId}, error: {ErrorMessage}", houseId, e.Message);
                return false;
            }
        }

        public async Task<List<HouseImage>> GetHouseImages(int houseId)
        {
            return await _db.HouseImages.Where(img => img.HouseId == houseId).ToListAsync();
        }


















        public async Task<IEnumerable<House>?> GetAll()
        {
            try
            {
                return await _db.Houses
                    .Include(h => h.Owner)
                    .Include(h => h.Images)  // 🔥 Inkluderer bildene til huset!
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("[HouseRepository] Failed to get all houses, error message: {e}", e.Message);
                return null;
            }
        }


        public async Task<House?> GetHouseById(int id)
        {
            try
            {
                return await _db.Houses
                    .Include(h => h.Owner)
                    .Include(h => h.Images) // 🔥 Henter også bilder
                    .FirstOrDefaultAsync(h => h.HouseId == id);
            }
            catch (Exception e)
            {
                _logger.LogError("[HouseRepository] Failed to get House by ID {HouseId}, error: {e}", id, e.Message);
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

