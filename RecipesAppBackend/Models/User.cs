using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Models
{
    public class User
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        [EmailAddress] 
        public string Email { get; set; }

        [Required] 
        [MinLength(8)] 
        public string PasswordHash { get; set; }
    }
}
