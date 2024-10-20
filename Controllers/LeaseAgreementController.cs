using HouseRental.DAL;
using HouseRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseRental.Controllers
{
    public class LeaseAgreementController : Controller
    {
        private readonly ILeaseAgreementRepository _leaseAgreementRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly IRenterRepository _enterRepository;
        private readonly ILogger<LeaseAgreementController> _logger;

        public LeaseAgreementController(ILeaseAgreementRepository leaseAgreementRepository, IHouseRepository houseRepository, IRenterRepository renterRepository, ILogger<LeaseAgreementController> logger)
        {
            _leaseAgreementRepository = leaseAgreementRepository;
            _houseRepository = houseRepository;
            _enterRepository = renterRepository;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Table()
        {
            try
            {
                var  leaseAgreements = await _leaseAgreementRepository.GetAll();
                if (leaseAgreements == null)
                {
                    _logger.LogError("[LeaseAgreementController] Error while fetching lease agreements in Table method.");
                    return NotFound();
                }
                return View(leaseAgreements);
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementController] Error in Table method. Error Message: {ex}", ex.Message);
                return StatusCode(500); // Internal Server Error
            }
        }



        [HttpGet]
        [Authorize]
        public async  Task<IActionResult> CreateNewLeaseAgreement()
        {
            try
            {
                var houseList = await _houseRepository.GetAll();
                var renterList = await _enterRepository.GetAll();

                ViewBag.HouseList = new SelectList(houseList, "HouseId", "Address");
                ViewBag.RenterList = new SelectList(renterList, "RenterId", "Name");

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementController] Error in CreateNewLeaseAgreement (GET) method. Error Message: {ex}", ex.Message);
                return StatusCode(500); 
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNewLeaseAgreement(LeaseAgreement leaseAgreement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _leaseAgreementRepository.CreateNewLeaseAgreement(leaseAgreement);
                    return RedirectToAction(nameof(Table));
                }
                return View(leaseAgreement);
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementController] Error in CreateNewLeaseAgreement (POST) method. Error Message: {ex}", ex.Message);
                return StatusCode(500); 
            }
        }




        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var leaseAgreement = await _leaseAgreementRepository.GetLeaseAgreementById(id);
                var houseList = await _houseRepository.GetAll();
                var renterList = await _enterRepository.GetAll();
                if (leaseAgreement == null)
                {
                    return NotFound();
                }
                ViewBag.HouseList = new SelectList(houseList, "HouseId", "Address");
                ViewBag.RenterList = new SelectList(renterList, "RenterId", "Name");
                return View(leaseAgreement);
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementController] Error in Update (GET) method. Error Message: {ex}", ex.Message);
                return StatusCode(500); // Internal Server Error
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(LeaseAgreement leaseAgreement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _leaseAgreementRepository.Update(leaseAgreement);
                    return RedirectToAction(nameof(Table));
                }
                return View(leaseAgreement);
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementController] Error in Update (POST) method. Error Message: {ex}", ex.Message);
                return StatusCode(500); // Internal Server Error
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var leaseAgreement = await _leaseAgreementRepository.GetLeaseAgreementById(id);
                if (leaseAgreement == null)
                {
                    _logger.LogError("[LeaseAgreementController] LeaseAgreement Id  not found for the LeaseAgreementId {LeaseAgreementId:0000}", id);
                    return BadRequest("LeaseAgreement not found for the LeaseAgreementId");
                }
                return View(leaseAgreement);
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementController] Error in Delete (GET) method. Error Message: {ex}", ex.Message);
                return StatusCode(500); // Internal Server Error
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _leaseAgreementRepository.Delete(id);
                if (result)
                {
                    return RedirectToAction(nameof(Table));
                }
                else
                {
                    _logger.LogError("[LeaseAgreementController] Delete operation failed for LeaseAgreementId: {id}", id);
                    return BadRequest("Failed to delete lease agreement.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("[LeaseAgreementController] Error in DeleteConfirmed (POST) method. Error Message: {ex}", ex.Message);
                return StatusCode(500); // Internal Server Error
            }
        }

    }
}
