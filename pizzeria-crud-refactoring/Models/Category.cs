using System.ComponentModel.DataAnnotations;

namespace pizzeria_crud_refactoring.Models
{
    public class Category
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il titolo non puo superare i 50 caratteri")]
        public string Name { get; set; }

        // per relazione 1:N e per default di MVC rendiamoli nullable
        public List<Pizza>? Pizzas { get; set; }

        public Category() { }
    }
}
