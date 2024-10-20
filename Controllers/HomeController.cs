using HouseRental.DAL;
using HouseRental.Models;
using HouseRental.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRental.Controllers
{
    public class HomeController : Controller
    {
        


        // Get_ /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }





        //Må lage en Grid view for første siden.
        //1 en metode for å behandle Grid.
        //2.lage HomeRepository og IHomeRepository
        //3.lage egent grid for første siden.
    }
}
