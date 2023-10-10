using la_mia_pizzeria_crud_mvc.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace pizzeria_crud_refactoring.Models
{
    public class Pizza
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(15, ErrorMessage = "Il nome massimo 15 caratteri dai su")]
        public string Name { get; set; }

        [Required(ErrorMessage = "la descrizione è obbligatoria")]
        [DescriptionValidation]
        [StringLength(1000, ErrorMessage = "massimo 1000 caratteri")]
        public string Description { get; set; }

        [Url]
        [Required(ErrorMessage = "L'URL è obbligatorio")]
        public string Photo { get; set; }

        [PriceValidation]
        [Required]
        public double Price { get; set; }


        //relazione 1:N con category
        public long? CategoryId { get; set; }
        public Category? Category { get; set; }


        //relazione N:N
        public List<Ingredient>? Ingredients { get; set; }



        public Pizza() { }

        public Pizza(string name, string description, string photo, double price)
        {
            Name = name;
            Description = description;
            Photo = photo;
            Price = price;
        }
    }
}
