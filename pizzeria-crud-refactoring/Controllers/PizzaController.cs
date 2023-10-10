using Microsoft.AspNetCore.Mvc;
using pizzeria_crud_refactoring.Models;
using pizzeria_mvc.Database;

namespace pizzeria_crud_refactoring.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;
        private PizzaContext _db = new PizzaContext();

        public PizzaController(ILogger<PizzaController> logger)
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
            Pizza? pizza = _db.Pizza.Where(x => x.Id == id).FirstOrDefault();
            return View(pizza);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if(!ModelState.IsValid) return View(pizza);

            _db.Pizza.Add(pizza);
            _db.SaveChanges();

            TempData["Message"] = $"La pizza {pizza.Name} è stata creata con successo";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            Pizza? pizza = _db.Pizza.Where(p => p.Id == id).FirstOrDefault();
            if (pizza == null) return View("../NotFound");

            else return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, Pizza pizza)
        {
            if (!ModelState.IsValid) return View(pizza);

            Pizza? pizzaToEdit = _db.Pizza.Where(p => p.Id == id).FirstOrDefault();

            if (pizzaToEdit == null)
                return View("../NotFound");

            else
            {
                pizzaToEdit.Price = pizza.Price;
                pizzaToEdit.Description = pizza.Description;
                pizzaToEdit.Name = pizza.Name;
                pizzaToEdit.Photo = pizza.Photo;
                _db.SaveChanges();
                TempData["Message"] = $"La pizza {pizzaToEdit.Name} è stata modificata correttamente";

                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            Pizza? pizzaToDelte = _db.Pizza.Where(P => P.Id == id).FirstOrDefault();

            if (pizzaToDelte == null) return View("../NotFound");

            _db.Pizza.Remove(pizzaToDelte);
            _db.SaveChanges();
            TempData["Message"] = $"La pizza {pizzaToDelte.Name} è stata eliminata correttamente";

            return RedirectToAction("Index");
        }
    }
}