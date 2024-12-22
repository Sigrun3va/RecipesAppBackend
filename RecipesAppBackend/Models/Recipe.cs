using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string IngredientsJson { get; set; } 
        [Required]
        public string CategoryJson { get; set; }    
        [Required]
        public string Instructions { get; set; }
        public string ImagePath { get; set; } = "assets/images/comingsoon.jpg"; 
        public bool IsUserAdded { get; set; }
    }
}
