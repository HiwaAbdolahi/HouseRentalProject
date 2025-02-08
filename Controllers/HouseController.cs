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
        public async Task<IActionResult> Details(int id)
        {
            var house = await _houseRepository.GetHouseById(id);
            if (house == null)
            {
                _logger.LogError("[HouseController] House not found while retrieving HouseId {HouseId:0000}", id);
                return NotFound("House Not Found!");
            }

            var viewModel = new DetailsViewModel(house)
            {
                IsHouse = true
            };

            return View(viewModel);
        }







        [HttpGet]
        [Authorize] 
        public IActionResult Create()
        {
            // Hent en liste over eiere fra databasen og lagre den i ViewBag
            ViewBag.OwnerList = new SelectList(_houseDbContext.Owners, "OwnerId", "Name");

            return View(new HouseUploadViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(HouseUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map ViewModel til House-modellen
                var house = new House
                {
                    Address = model.Address,
                    Price = model.Price,
                    Rooms = model.Rooms,
                    IsAvailable = model.IsAvailable,
                    OwnerId = model.OwnerId
                };

                bool houseCreated = await _houseRepository.Create(house);
                if (houseCreated)
                {
                    var imageUrls = new List<string>();

                    if (model.Images != null && model.Images.Any())
                    {
                        foreach (var image in model.Images)
                        {
                            var filePath = Path.Combine("wwwroot/images", image.FileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }
                            imageUrls.Add("/images/" + image.FileName);
                        }
                    }

                    if (imageUrls.Any())
                    {
                        await _houseRepository.AddHouseImages(house.HouseId, imageUrls);
                    }

                    return RedirectToAction(nameof(Table));
                }
            }

            _logger.LogError("[HouseController] House creation failed {@model}", model);
            return View(model);
        }






















        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var house = await _houseRepository.GetHouseById(id);
            if (house == null)
            {
                _logger.LogError("[HouseController] House not found while updating HouseId {HouseId:0000}", id);
                return NotFound("House not found for the HouseId");
            }

            // Mapper husdata til ViewModel
            var viewModel = new HouseUploadViewModel
            {
                HouseId = house.HouseId,
                Address = house.Address,
                Price = house.Price,
                Rooms = house.Rooms,
                IsAvailable = house.IsAvailable,
                ExistingImages = house.Images
            };

            return View(viewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(HouseUploadViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Hent eksisterende hus fra databasen
                var house = await _houseRepository.GetHouseById(viewModel.HouseId);
                if (house == null)
                {
                    _logger.LogError("[HouseController] House not found for update with HouseId {HouseId:0000}", viewModel.HouseId);
                    return NotFound("House not found for the HouseId");
                }

                // Oppdater husinformasjonen
                house.Address = viewModel.Address;
                house.Price = viewModel.Price;
                house.Rooms = viewModel.Rooms;
                house.IsAvailable = viewModel.IsAvailable;

                bool updateResult = await _houseRepository.Update(house);
                if (updateResult)
                {
                    // Håndter nye bilder hvis de er lastet opp
                    if (viewModel.Images != null && viewModel.Images.Any())
                    {
                        var imageUrls = new List<string>();
                        foreach (var image in viewModel.Images)
                        {
                            if (image.Length > 0)
                            {
                                var filePath = Path.Combine("wwwroot/images", image.FileName);
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await image.CopyToAsync(stream);
                                }
                                imageUrls.Add("/images/" + image.FileName);
                            }
                        }

                        if (imageUrls.Any())
                        {
                            await _houseRepository.AddHouseImages(house.HouseId, imageUrls);
                        }
                    }

                    return RedirectToAction(nameof(Table));
                }
            }

            _logger.LogError("[HouseController] House update failed for HouseId {HouseId:0000}", viewModel.HouseId);
            return View(viewModel);
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
