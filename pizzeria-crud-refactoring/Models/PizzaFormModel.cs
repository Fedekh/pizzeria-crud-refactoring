using Microsoft.AspNetCore.Mvc.Rendering;

namespace pizzeria_crud_refactoring.Models
{
    public class PizzaFormModel
    {
        public Pizza? Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }

        public List<string>? SelectedIngredients { get; set; }
    }
}
