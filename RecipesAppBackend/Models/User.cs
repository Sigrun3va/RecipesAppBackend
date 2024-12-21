using RecipesApp.Models;

namespace RecipesApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
