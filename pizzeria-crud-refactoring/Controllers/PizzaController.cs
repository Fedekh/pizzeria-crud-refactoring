using la_mia_pizzeria_crud_mvc.CustomLogger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pizzeria_crud_refactoring.Models;
using pizzeria_mvc.Database;

namespace pizzeria_crud_refactoring.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class PizzaController : Controller
    {
        private ICustomLog _myLogger;
        private PizzaContext _db;


        public PizzaController(ICustomLog log, PizzaContext db)
        {
            _myLogger = log;
            _db = db;

        }

        [HttpGet]
        public IActionResult Index()
        {
            _myLogger.WriteLog($"L'utente è arrivato nel admins index");
            List<Pizza> pizzas = _db.Pizza.ToList();
            return View(pizzas);
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
           Pizza pizza = _db.Pizza.Where(p=>p.Id == id).Include(p=>p.Category).Include(p=>p.Ingredients).FirstOrDefault();

            if (pizza == null) return View("../NotFound");
            _myLogger.WriteLog($"L'utente è arrivato sta visualizzando i dettagli di {pizza.Name}");
            return View(pizza);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _db.Category.ToList();
            List<Ingredient> ingredientsDB = _db.Ingredient.ToList();

            List<SelectListItem> listIngredients = new List<SelectListItem>();

            foreach(Ingredient ingredient in ingredientsDB)
            {
                listIngredients.Add(new SelectListItem()
                {
                    Text = ingredient.Name,
                    Value = ingredient.Id.ToString()
                });
            }

            PizzaFormModel model = new PizzaFormModel()
            {
                Pizza = new Pizza(),
                Categories = categories,
                Ingredients = listIngredients
            };
            _myLogger.WriteLog($"L'utente vuole creare una pizza");
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel model)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _db.Category.ToList();
                List<Ingredient> IngredientsDb = _db.Ingredient.ToList();
                List<SelectListItem> selectListItem = new List<SelectListItem>();


                foreach (Ingredient ingredient in IngredientsDb)
                {
                    selectListItem.Add(new SelectListItem()
                    {
                        Text = ingredient.Name,
                        Value = ingredient.Id.ToString(),
                    });
                }
                model.Categories = categories;
                model.Ingredients = selectListItem;

                return View(model);
            }

            model.Pizza.Ingredients = new List<Ingredient>();

            if (model.SelectedIngredients != null)
            {
                foreach (string ingredientId in model.SelectedIngredients)
                {
                    long ingredintLongId = long.Parse(ingredientId);

                    Ingredient? ingredientInDb = _db.Ingredient.Where(m => m.Id == ingredintLongId).FirstOrDefault();
                    if (ingredientInDb != null)
                    {
                        model.Pizza.Ingredients.Add(ingredientInDb);
                    }
                }
            }


                _db.Pizza.Add(model.Pizza);
                _db.SaveChanges();

                TempData["Message"] = $"La pizza {model.Pizza.Name} è stata creata con successo";
                _myLogger.WriteLog($"L'utente ha creato {model.Pizza.Name}");

            return RedirectToAction("Index");
            
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(long id)
        {
            Pizza? pizzaEdit = _db.Pizza.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();

            if (pizzaEdit != null)
            {
                List<Category> categories = _db.Category.ToList();
                List<Ingredient> IngredientsDb = _db.Ingredient.ToList();
                List<SelectListItem> selectListItem = new List<SelectListItem>();


                foreach (Ingredient ingredient in IngredientsDb)
                {
                    selectListItem.Add(new SelectListItem()
                    {
                        Text = ingredient.Name,
                        Value = ingredient.Id.ToString(),
                        Selected = pizzaEdit.Ingredients.Any(p => p.Id == ingredient.Id)
                    });
                }

                PizzaFormModel model = new PizzaFormModel()
                {
                    Pizza = pizzaEdit,
                    Categories = categories,
                    Ingredients = selectListItem
                };

                _myLogger.WriteLog($"L'utente vuole modificare {model.Pizza.Name}");

                return View(model);
            }

            else
                return View("../NotFound");

        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, PizzaFormModel model)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _db.Category.ToList();
                List<Ingredient> IngredientsDb = _db.Ingredient.ToList();
                List<SelectListItem> selectListItem = new List<SelectListItem>();


                foreach (Ingredient ingredient in IngredientsDb)
                {
                    selectListItem.Add(new SelectListItem()
                    {
                        Text = ingredient.Name,
                        Value = ingredient.Id.ToString(),
                    });
                }
                model.Categories = categories;
                model.Ingredients = selectListItem;

                return View(model); 
            }

            Pizza? pizzaToEdit = _db.Pizza.Where(p => p.Id == id).Include(p=>p.Ingredients).FirstOrDefault();

            if (pizzaToEdit == null)
                return View("../NotFound");

            else
            {
                pizzaToEdit.Ingredients.Clear();  //sempre

                model.Pizza.Ingredients = new List<Ingredient>();


                if (model.SelectedIngredients != null)
                {
                    foreach (string ingredientId in model.SelectedIngredients)
                    {
                        long ingredintLongId = long.Parse(ingredientId);

                        Ingredient? ingredientInDb = _db.Ingredient.Where(m => m.Id == ingredintLongId).FirstOrDefault();
                        if (ingredientInDb != null)
                        {
                            model.Pizza.Ingredients.Add(ingredientInDb);
                        }
                    }
                }

                pizzaToEdit.Price = model.Pizza.Price;
                pizzaToEdit.Description = model.Pizza.Description;
                pizzaToEdit.Name = model.Pizza.Name;
                pizzaToEdit.Photo = model.Pizza.Photo;
                pizzaToEdit.CategoryId = model.Pizza.CategoryId;
                pizzaToEdit.Ingredients = model.Pizza.Ingredients;
                _db.SaveChanges();
                TempData["Message"] = $"La pizza {pizzaToEdit.Name} è stata modificata correttamente";
                _myLogger.WriteLog($"L'utente ha modificato {model.Pizza.Name}");

                return RedirectToAction("Index");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            Pizza? pizzaToDelte = _db.Pizza.Where(P => P.Id == id).FirstOrDefault();

            if (pizzaToDelte == null) return View("../NotFound");

            _db.Pizza.Remove(pizzaToDelte);
            _db.SaveChanges();
            TempData["Message"] = $"La pizza {pizzaToDelte.Name} è stata eliminata correttamente";
            _myLogger.WriteLog($"L'utente ha eliminato {pizzaToDelte.Name}");

            return RedirectToAction("Index");
        }
    }
}