using la_mia_pizzeria_crud_mvc.CustomLogger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizzeria_crud_refactoring.Models;
using pizzeria_mvc.Database;

namespace pizzeria_crud_refactoring.Controllers
{
    public class HomeController : Controller
    {
        private ICustomLog _myLogger;
        private PizzaContext _db;


        public HomeController(ICustomLog log, PizzaContext db)
        {
            _myLogger = log;
            _db = db;

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
            Pizza pizza = _db.Pizza.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();

            if (pizza == null) return View("../NotFound");
            return View(pizza);
        }


    }
}