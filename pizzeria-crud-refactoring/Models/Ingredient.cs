namespace pizzeria_crud_refactoring.Models
{
    public class Ingredient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Pizza>? Pizzas { get; set; }

        public Ingredient() { }
    }
}
