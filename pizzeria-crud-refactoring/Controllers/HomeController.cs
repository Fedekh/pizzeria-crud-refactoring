using Microsoft.AspNetCore.Mvc;
using pizzeria_crud_refactoring.Models;
using pizzeria_mvc.Database;

namespace pizzeria_crud_refactoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private PizzaContext _db = new PizzaContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> pizzas = _db.Pizza.ToList();
            return View(pizzas);
        }


        [HttpGet]
        public IActionResult Details(long id)
        {
            Pizza? pizza = _db.Pizza.Where(p=>p.Id == id).FirstOrDefault();

            if (pizza == null) return View("../NotFound");

            return View(pizza);

        }


    }
}