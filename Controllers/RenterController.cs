using HouseRental.DAL;
using HouseRental.Models;
using HouseRental.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseRental.Controllers
{
    public class RenterController : Controller
    {
        private readonly IRenterRepository _renterRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly HouseDbContext _houseDbContext;
        private readonly ILogger<RenterController> _logger;

        public RenterController(IRenterRepository renterRepository, IHouseRepository houseRepository, HouseDbContext houseDbContext, ILogger<RenterController> logger)
        {
            _renterRepository = renterRepository;
            _houseRepository = houseRepository;
            _houseDbContext = houseDbContext;
            _logger = logger;
        }



        [Authorize]
        public async Task<IActionResult> DetailsForRenter(int id)
        {
            try
            {
                var house = await _houseRepository.GetHouseById(id);
                if (house == null)
                {
                    _logger.LogError("[RenterController] House not found while fetching details for HouseId {HouseId:0000}", id);
                    return NotFound("House Not Found!");
                }

                // Bruk DetailsViewModel med IsRenter satt til true
                var detailsViewModel = new DetailsViewModel(house)
                {
                    IsRenter = true,
                    IsHouse = false,
                    IsOwner = false
                };

                return View(detailsViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError("[RenterController] An error occurred while fetching house details: {e}", e.Message);
                return BadRequest("An error occurred while fetching house details.");
            }
        }



        [Authorize]
        public async Task<IActionResult> Table()
        {
            try
            {
                var renters = await _renterRepository.GetAll();
                return View(renters);
            }
            catch (Exception ex)
            {
                _logger.LogError("[RenterController] Error while fetching renters for the table. Error Message: {ex}", ex.Message);
                return View(new List<Renter>()); // Returnerer en tom liste eller en annen passende feilvisning
            }
        }



        [HttpGet]
        [Authorize]
        public IActionResult CreateNewRenter()
        {
            try
            {
                // Hent en liste over Hus fra databasen og lagre den i ViewBag
                ViewBag.HouseList = new SelectList(_houseDbContext.Houses, "HouseId", "Address");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("[RenterController] Error while fetching houses for creating a new renter. Error Message: {ex}", ex.Message);
                return View(new Renter()); // Returnerer en tom rentermodell eller en annen passende feilvisning
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNewRenter(Renter renter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _renterRepository.Create(renter);
                    return RedirectToAction(nameof(Table));
                }
                catch (Exception ex)
                {
                    _logger.LogError("[RenterController] Error while creating a new renter. Error Message: {ex}", ex.Message);
                    return View(renter); // Returnerer samme rentermodell med feilmeldinger eller en annen passende feilvisning
                }
            }
            return View(renter);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                // Hent en liste over Hus fra databasen og lagre den i ViewBag
                ViewBag.HouseList = new SelectList(_houseDbContext.Houses, "HouseId", "Address");
                var renter = await _renterRepository.GetRenterById(id);
                if (renter == null)
                {
                    return NotFound();
                }
                return View(renter);
            }
            catch (Exception ex)
            {
                _logger.LogError("[RenterController] Error while fetching houses for updating renter or renter not found. Error Message: {ex}", ex.Message);
                return NotFound(); // Returnerer NotFound hvis det oppstår en feil
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(Renter renter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _renterRepository.Update(renter);
                    return RedirectToAction(nameof(Table));
                }
                catch (Exception ex)
                {
                    _logger.LogError("[RenterController] Error while updating a renter. Error Message: {ex}", ex.Message);
                    return View(renter); // Returnerer samme rentermodell med feilmeldinger eller en annen passende feilvisning
                }
            }
            return View(renter);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var renter = await _renterRepository.GetRenterById(id);
                if (renter == null)
                {
                    return NotFound();
                }
                return View(renter);
            }
            catch (Exception ex)
            {
                _logger.LogError("[RenterController] Error while fetching renter for deletion or renter not found. Error Message: {ex}", ex.Message);
                return NotFound(); // Returnerer NotFound hvis det oppstår en feil
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool retrnOk = await _renterRepository.Delete(id);
                if (!retrnOk)
                {
                    _logger.LogError("[RenterController] Renter deletion failed for the RenterId {RenterId:0000}", id);
                    return BadRequest("Renter deletion failed");
                }
                return RedirectToAction(nameof(Table));
            }
            catch (Exception ex)
            {
                _logger.LogError("[RenterController] Error while deleting a renter. Error Message: {ex}", ex.Message);
                return BadRequest("Error while deleting the renter."); // Returnerer BadRequest hvis det oppstår en feil
            }
        }
    }
}
