using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Details(int id) //http://localhost/movies/details/1
        {
            return View();
        }
    }
}
