using System.Security.Permissions;
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
                default:
                    throw new ArgumentOutOfRangeException(
                        "Please insert one of the initials that you can see on the console!");
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e.ParamName);
            Console.ReadKey();
            Environment.Exit(0);
        }
        catch (ArgumentOutOfRangeException e)
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
        Console.WriteLine("Here are the available recipes");
        DisplayRecipes();
        Console.WriteLine();
        Console.WriteLine("Write the name of the recipe.");
        Console.WriteLine();
        string userInput = Console.ReadLine();

        List<IngredientClass> ingredientsFromSpecifiedRecipe = IngredientsFromSpecifiedRecipe(userInput);

        Console.WriteLine();
        foreach (var item in ingredientsFromSpecifiedRecipe)
            Console.WriteLine(item.Name);

    }

    private List<IngredientClass> IngredientsFromSpecifiedRecipe(string? userInput)
    {
        List<RecipeClass> selectedRecipe = Recipes
            .FindAll(recipe =>
                recipe.Name
                    .Equals(userInput, StringComparison.OrdinalIgnoreCase));

        List<int> specificRecipeIds = selectedRecipe
            .First().IngredientIds;

        List<IngredientClass> ingredientsFromSpecifiedRecipe = Ingredients
            .Where(ingredient => specificRecipeIds
                .Contains(ingredient.Id)).ToList();
        return ingredientsFromSpecifiedRecipe;
    }

    public void DisplayRecipes()
    {
        try
        {
            if (!Recipes.Any())
                throw new ArgumentNullException("Liste ist noch leer!");

            foreach (var recipe in Recipes)
            {
                Console.WriteLine(@$"{recipe.Id}. {recipe.Name}");
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.ParamName);
        }
    }

    public async Task<List<IngredientClass>> GetIngredients()
    {

        try
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var jsonFilePath = Path.Combine(basePath, @"..\..\..\FilesWithContentJSON\ingredients.json");
            var jsonString = File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<IngredientClass>>(jsonString);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null;
    }

    public async Task<List<RecipeClass>> GetRecipes()
    {

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var jsonFilePath = Path.Combine(basePath, @"..\..\..\FilesWithContentJSON\recipes.json");
        var jsonString = File.ReadAllText(jsonFilePath);
        return JsonSerializer.Deserialize<List<RecipeClass>>(jsonString);
    }


}

