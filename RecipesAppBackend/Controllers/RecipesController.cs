using Microsoft.AspNetCore.Mvc;
using RecipesApp.Data;
using Newtonsoft.Json;
using RecipesApp.Models;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly AppDbContext _context;

    public RecipesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddRecipe([FromBody] Recipe recipe)
    {
        try
        {
            if (recipe == null ||
                string.IsNullOrEmpty(recipe.Name) ||
                string.IsNullOrEmpty(recipe.Description) ||
                string.IsNullOrEmpty(recipe.Instructions) ||
                string.IsNullOrEmpty(recipe.IngredientsJson) ||
                string.IsNullOrEmpty(recipe.CategoryJson))
            {
                return BadRequest(new { message = "Invalid recipe data." });
            }

            if (string.IsNullOrEmpty(recipe.ImagePath))
            {
                recipe.ImagePath = "assets/images/comingsoon.jpg";
            }

            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return Ok(new { message = "Recipe added successfully!", recipe });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the recipe.", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult EditRecipe(int id, [FromBody] Recipe updatedRecipe)
    {
        try
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound(new { message = "Recipe not found." });
            }

            if (updatedRecipe == null ||
                string.IsNullOrWhiteSpace(updatedRecipe.Name) ||
                string.IsNullOrWhiteSpace(updatedRecipe.Description) ||
                string.IsNullOrWhiteSpace(updatedRecipe.Instructions) ||
                string.IsNullOrWhiteSpace(updatedRecipe.IngredientsJson) ||
                string.IsNullOrWhiteSpace(updatedRecipe.CategoryJson))
            {
                return BadRequest(new { message = "Invalid recipe data." });
            }
            
            recipe.Name = updatedRecipe.Name;
            recipe.Description = updatedRecipe.Description;
            recipe.IngredientsJson = updatedRecipe.IngredientsJson;
            recipe.CategoryJson = updatedRecipe.CategoryJson;
            recipe.Instructions = updatedRecipe.Instructions;
            recipe.ImagePath = string.IsNullOrWhiteSpace(updatedRecipe.ImagePath)
                ? "assets/images/comingsoon.jpg"
                : updatedRecipe.ImagePath;
            recipe.IsUserAdded = updatedRecipe.IsUserAdded;

            _context.SaveChanges();

            return Ok(new { message = "Recipe updated successfully!", recipe });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the recipe.", error = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult GetRecipes()
    {
        try
        {
            var recipes = _context.Recipes
                .Select(r => new
                {
                    id = r.Id,
                    name = r.Name,
                    description = r.Description,
                    ingredients = string.IsNullOrWhiteSpace(r.IngredientsJson) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(r.IngredientsJson),
                    instructions = r.Instructions,
                    category = string.IsNullOrWhiteSpace(r.CategoryJson) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(r.CategoryJson),
                    imagePath = r.ImagePath,
                    isUserAdded = r.IsUserAdded
                })
                .ToList();

            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while retrieving recipes.",
                error = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRecipe(int id)
    {
        try
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound(new { message = "Recipe not found." });
            }

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            return Ok(new { message = "Recipe deleted successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the recipe.", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetRecipeById(int id)
    {
        try
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
            {
                return NotFound(new { message = "Recipe not found." });
            }

            var result = new
            {
                id = recipe.Id,
                name = recipe.Name,
                description = recipe.Description,
                ingredients = string.IsNullOrWhiteSpace(recipe.IngredientsJson) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(recipe.IngredientsJson),
                instructions = recipe.Instructions,
                category = string.IsNullOrWhiteSpace(recipe.CategoryJson) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(recipe.CategoryJson),
                imagePath = recipe.ImagePath,
                isUserAdded = recipe.IsUserAdded
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the recipe.", error = ex.Message });
        }
    }
}
