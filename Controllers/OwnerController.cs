using HouseRental.DAL;
using Microsoft.AspNetCore.Mvc;
using HouseRental.Models;
using Microsoft.AspNetCore.Authorization;

namespace HouseRental.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly ILogger<OwnerController> _logger;
        
        public OwnerController(IOwnerRepository ownerRepository, IHouseRepository houseRepository, ILogger<OwnerController> logger)
        {
            _ownerRepository = ownerRepository;
            _houseRepository = houseRepository;
            _logger = logger;
        }





        [Authorize]
        public async Task<IActionResult> DetailsHouseForOwner(int id)
        {
            try
            {
                var house = await _houseRepository.GetHouseById(id);
                if (house == null)
                {
                    _logger.LogError("[OwnerController] House not found while executing GetHouseById for HouseId {HouseId:0000}", id);
                    return NotFound("House Not Found!");
                }

                return View(house);
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerController] An error occurred while fetching house details: {e}", e.Message);
                return BadRequest("An error occurred while fetching house details.");
            }
        }



        [Authorize]
        public async Task<IActionResult> Table()
        {
            try
            {
                var owners = await _ownerRepository.GetAll();
                return View(owners);
            }
            catch (Exception e) 
            {
                _logger.LogError("[OwnerController] An error occurred while fetching owner data: {e}", e.Message);
                return BadRequest("An error occurred while fetching owner data.");
            }
            
        }

        
        
        [HttpGet]
        [Authorize]
        public IActionResult CreateNewOwner()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNewOwner(Owner owner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _ownerRepository.Create(owner);
                    return RedirectToAction(nameof(Table));
                }
                return View(owner);
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerController] An error occurred while creating a new owner: {e}", e.Message);
                return BadRequest("An error occurred while creating a new owner.");
            }
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var owner = await _ownerRepository.GetOwnerById(id);
                if (owner == null)
                {
                    return NotFound();
                }
                return View(owner);
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerController] An error occurred while updating owner (GET) for OwnerId {OwnerId:0000}: {e}", id, e.Message);
                return BadRequest("An error occurred while updating the owner.");
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(Owner owner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _ownerRepository.Update(owner);
                    return RedirectToAction(nameof(Table));
                }
                return View(owner);
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerController] An error occurred while updating owner (POST) for OwnerId {OwnerId:0000}: {e}", owner.OwnerId, e.Message);
                return BadRequest("An error occurred while updating the owner.");
            }
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var owner = await _ownerRepository.GetOwnerById(id);
                if (owner == null)
                {
                    return NotFound();
                }
                return View(owner);
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerController] An error occurred while deleting owner (GET) for OwnerId {OwnerId:0000}: {e}", id, e.Message);
                return BadRequest("An error occurred while deleting the owner.");
            }
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool returnOk = await _ownerRepository.Delete(id);
                if (!returnOk)
                {
                    _logger.LogError("[OwnerController] Owner deletion failed for the OwnerId {OwnerId:0000}", id);
                    return BadRequest("Owner deletion failed");
                }
                return RedirectToAction(nameof(Table));
            }
            catch (Exception e)
            {
                _logger.LogError("[OwnerController] An error occurred while deleting owner (POST) for OwnerId {OwnerId:0000}: {e}", id, e.Message);
                return BadRequest("An error occurred while deleting the owner.");
            }
        }



    }
}
