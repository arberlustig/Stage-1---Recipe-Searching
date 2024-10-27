﻿using Stage_1___Recipe_Searching.Interfaces;
using System.Text.Json;

public class SearchQuery : ISearchQuery
{

    public List<IngredientClass> Ingredients { get; set; } = new List<IngredientClass>();
    public List<RecipeClass> Recipes { get; set; } = new List<RecipeClass>();

    public async Task Initialize()
    {
        Ingredients = await GetIngredients();
        Recipes = await GetRecipes();
    }
    public void DisplayRecipes()
    {
        if(!Recipes.Any())
            Console.WriteLine("nothing");

        foreach (var recipe in Recipes)
        {
            Console.WriteLine(@$"{recipe.Id}. {recipe.Name}");
        }
    }

    public async Task<List<IngredientClass>> GetIngredients()
    {
       
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var jsonFilePath = Path.Combine(basePath, @"..\..\..\FilesWithContentJSON\ingredients.json");
        var jsonString = File.ReadAllText(jsonFilePath); 
        return JsonSerializer.Deserialize<List<IngredientClass>>(jsonString);
    }

    public async Task<List<RecipeClass>> GetRecipes()
    {
       
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var jsonFilePath = Path.Combine(basePath, @"..\..\..\FilesWithContentJSON\recipes.json");
        var jsonString = File.ReadAllText(jsonFilePath); 
        return JsonSerializer.Deserialize<List<RecipeClass>>(jsonString);
    }

    
}
