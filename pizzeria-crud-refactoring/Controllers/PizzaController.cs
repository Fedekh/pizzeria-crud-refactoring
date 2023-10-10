using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
           Pizza pizza = _db.Pizza.Where(p=>p.Id == id).Include(p=>p.Category).FirstOrDefault();

            if (pizza == null) return View("../NotFound");
            return View(pizza);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _db.Category.ToList();

            PizzaFormModel model = new PizzaFormModel()
            {
                Pizza = new Pizza(),
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel model)
        {
            if(!ModelState.IsValid) return View(model);

            Pizza pizzaCreate = new Pizza()
            {
                Name = model.Pizza.Name,
                Description = model.Pizza.Description,
                Photo = model.Pizza.Photo,
                Price = model.Pizza.Price
            };

            pizzaCreate.CategoryId = model.Pizza.CategoryId;

            _db.Pizza.Add(pizzaCreate);
            _db.SaveChanges();

            TempData["Message"] = $"La pizza {pizzaCreate.Name} è stata creata con successo";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            Pizza? pizza = _db.Pizza.Where(p => p.Id == id).FirstOrDefault();
            if (pizza == null) return View("../NotFound");

            else
            {
                List<Category> categories = _db.Category.ToList(); 
                PizzaFormModel model = new PizzaFormModel()
                {
                    Pizza = pizza,
                    Categories = categories
                };

                return View(model);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, PizzaFormModel model)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _db.Category.ToList();
                model.Categories = categories;
                return View(model); 
            }

            Pizza? pizzaToEdit = _db.Pizza.Where(p => p.Id == id).FirstOrDefault();

            if (pizzaToEdit == null)
                return View("../NotFound");

            else
            {
                pizzaToEdit.Price = model.Pizza.Price;
                pizzaToEdit.Description = model.Pizza.Description;
                pizzaToEdit.Name = model.Pizza.Name;
                pizzaToEdit.Photo = model.Pizza.Photo;
                pizzaToEdit.CategoryId = model.Pizza.CategoryId;
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