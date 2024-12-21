using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public string ImagePath { get; set; }
    public bool IsUserAdded { get; set; }

    [Required]
    public string IngredientsJson { get; set; }

    [Required]
    public string CategoryJson { get; set; }

    [NotMapped]
    public List<string> Ingredients
    {
        get => string.IsNullOrEmpty(IngredientsJson)
               ? new List<string>()
               : JsonConvert.DeserializeObject<List<string>>(IngredientsJson);
        set => IngredientsJson = JsonConvert.SerializeObject(value);
    }

    [NotMapped]
    public List<string> Category
    {
        get => string.IsNullOrEmpty(CategoryJson)
               ? new List<string>()
               : JsonConvert.DeserializeObject<List<string>>(CategoryJson);
        set => CategoryJson = JsonConvert.SerializeObject(value);
    }
}
