using Stage_1___Recipe_Searching.Interfaces;
using System.Text.Json;
using System.Threading.Channels;

public class SearchQuery : ISearchQuery
{

    public List<IngredientClass> Ingredients { get; set; } = new List<IngredientClass>();
    public List<RecipeClass> Recipes { get; set; } = new List<RecipeClass>();

    public async Task Initialize()
    {
        Ingredients = await GetIngredients();
        Recipes = await GetRecipes();
    }

    public void InputChoice(string userInput)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userInput))
                throw new ArgumentNullException("UserInput must be filled with the initials!");

            switch (userInput)
            {
                case "l":
                case "L":
                    DisplayRecipes();
                    break;
                case "r":
                case "R":
                    DisplayIngredientsForSpecifiedRecipe();
                    break;
                case "S":
                case "s":
                    RecipesForContainingSpecifiedIngredients();
                    break;
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e.ParamName);
            Console.ReadKey();
            Environment.Exit(0);
        }

    }

    private void RecipesForContainingSpecifiedIngredients()
    {
        throw new NotImplementedException();
    }

    private void DisplayIngredientsForSpecifiedRecipe()
    {
        //gib ein bestimmtes rezeptname ein
        //->also readline() zuerst
        //dann durch die liste der rezepte durchgehen
        //und dann auf die liste innerhalb eines rezeptes zugreifen und dann die id hernehmen und die liste der ingredients benutzen
        Console.WriteLine("Here are the available recipes");
        DisplayRecipes();
        Console.WriteLine();
        Console.WriteLine("Write the name of the recipe.");
        string userInput = Console.ReadLine();

        List<RecipeClass> selectedRecipe = Recipes.FindAll(recipe =>
            recipe.Name.Equals(userInput, StringComparison.OrdinalIgnoreCase));
        RecipeClass recipe = selectedRecipe.First();

        Console.ReadKey();


    }

    public void DisplayRecipes()
    {
        if (!Recipes.Any())
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

