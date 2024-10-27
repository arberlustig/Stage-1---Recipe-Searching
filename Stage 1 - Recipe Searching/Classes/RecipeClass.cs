using System.Text.Json;
using Stage_1___Recipe_Searching.Interfaces;

public class RecipeClass 
{
    public int Id { get; set; }              // "Id" im JSON
    public string Name { get; set; }          // "Name" im JSON
    public List<int> IngredientIds { get; set; } // "IngredientIds" im JSON

}