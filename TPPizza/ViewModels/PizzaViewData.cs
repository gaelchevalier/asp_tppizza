using BO;
using System.ComponentModel.DataAnnotations;

namespace TPPizza.ViewModels
{
    public class PizzaViewData
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Nom { get; set; }
        
        [Required]
        public Pate Pate { get; set; }
        public List<Pate> Pates { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        [Required]
        [MaxLength(5)]
        [MinLength(2)]
        public List<int> SelectedIngredients { get; set; }
    }
}
