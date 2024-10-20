using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HouseRental.DAL;
using HouseRental.Models;
using HouseRental.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseRental.Controllers
{
    public class HouseController : Controller
    {
        private readonly IHouseRepository _houseRepository;
        private readonly HouseDbContext _houseDbContext;
        private readonly ILogger<HouseController> _logger;

        public HouseController(IHouseRepository houseRepository, HouseDbContext houseDbContext, ILogger<HouseController> logger)
        {
            _houseRepository = houseRepository;
            _houseDbContext = houseDbContext;
            _logger = logger;
        }


        [Authorize]
        public async Task<IActionResult> Table()
        {
            
            var houses = await _houseRepository.GetAll();

            if (houses == null)
            {
                _logger.LogError("[HouseController] House list not Found while executing _HouseReposioty.GetAll()");
                return NotFound("House Not Found");
            }

            var houseListViewModel = new HouseListViewModel(houses, "Table");
            return View(houseListViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Grid() 
        {
            var houses = await _houseRepository.GetAll();

            if(houses == null)
            {
                _logger.LogError("[HouseController] House list not Found while executing _HouseReposioty.GetAll()");
                return NotFound("House Not Found");
            }
            var houseListViewModel = new HouseListViewModel(houses, "Grid");

            return View(houseListViewModel);
        }







        [Authorize]
        public async  Task<IActionResult> Details(int id) 
        {
            var house = await _houseRepository.GetHouseById(id);
            if (house == null)
            {
                _logger.LogError("[HouseController] House list not Found while executing _HouseReposioty.GetAll()");
                return BadRequest("House Not Found!");
                
            }
            return View(house);
        }




        [HttpGet]
        [Authorize] 
        public IActionResult Create()
        {
            // Hent en liste over eiere fra databasen og lagre den i ViewBag
            ViewBag.OwnerList = new SelectList(_houseDbContext.Owners, "OwnerId", "Name"); 

            return View();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(House house) 
        {
            if (ModelState.IsValid)
            {
                bool ReturnOk = await _houseRepository.Create(house);
                if (ReturnOk)
                return RedirectToAction(nameof(Table));
            }
            _logger.LogError("[HouseController] House creation failed {@house}", house);
            return View(house);
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var house = await _houseRepository.GetHouseById(id);
            if(house == null)
            {
                _logger.LogError("[HouseController] House not found while updating HouseId {HouseId:0000}", id);
                return NotFound("House not found for the HouseId");
            }
            return View(house);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(House house) 
        {
            if (ModelState.IsValid)
            {
                bool returnOk = await _houseRepository.Update(house);
                if(returnOk)
                    return RedirectToAction(nameof(Table));
            }
            _logger.LogError("[HouseController] House Update Failed {@House}", house);
            return View(house);
        }




        [HttpGet]
        [Authorize]
        public async  Task<IActionResult> Delete (int id)
        {
           
           var house = await _houseRepository.GetHouseById(id);

            if (house == null)
            {
                _logger.LogError("[HouseController] House not found for the HouseId {HouseId:0000}", id);
                return BadRequest("House not found for the HouseId");
            }
            return View(house);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            bool returnOk = await _houseRepository.Delete(id);
            if (!returnOk)
            {
                _logger.LogError("[HouseController] House deletion failed for the HouseId {HouseId:0000}", id);
                return BadRequest("House deletion failed");
            }



            return RedirectToAction(nameof(Table));
        }

        
    }
}
