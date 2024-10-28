using Stage_1___Recipe_Searching.Interfaces;
using System.Text.Json;

public class SearchQuery : ISearchQuery
{
    public List<IngredientClass> Ingredients { get; set; } = new List<IngredientClass>();
    public List<RecipeClass> Recipes { get; set; } = new List<RecipeClass>();

    public async Task Initialize()
    {
        Ingredients = await GetIngredients("ingredients.json");
        Recipes = await GetRecipes("recipes.json");
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
                    DisplayingRecipes();
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.ParamName);
            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(0);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.ParamName);
            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(0);
        }

    }
    private void RecipesForContainingSpecifiedIngredients()
    {
        Console.WriteLine();
        Console.WriteLine(@"
You can search for recipes that contain these specified ingredients!
Just type the name of the ingredient in!");
        DisplayingIngredients();
        Console.WriteLine();

        string userInput = Console.ReadLine();

        IngredientClass ingredientUserInput = Ingredients
            .Find(ingredient => ingredient.Name.Equals(userInput.TrimEnd(), StringComparison.OrdinalIgnoreCase));

        List<RecipeClass> recipes = Recipes
            .FindAll(recipe => recipe.IngredientIds.Contains(ingredientUserInput.Id));

        Console.WriteLine();

        foreach (var recipe in recipes)
            Console.WriteLine(recipe.Name);
        

    }
    public void DisplayingIngredients()
    {
        try
        {
            if (!Ingredients.Any())
                throw new ArgumentNullException("List of ingredients is still empty");

            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine(@$"{ingredient.Id}. {ingredient.Name}");
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ParamName);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
    private void DisplayIngredientsForSpecifiedRecipe()
    {
        Console.WriteLine("Here are the available recipes");
        Console.WriteLine();
        DisplayingRecipes();
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
        List<int> selectedRecipe = Recipes
            .Find(recipe =>
                recipe.Name
                    .Equals(userInput.TrimEnd(), StringComparison.OrdinalIgnoreCase))
            .IngredientIds;

        List<IngredientClass> ingredientsFromSpecifiedRecipe = Ingredients
            .Where(ingredient => selectedRecipe
                .Contains(ingredient.Id)).ToList();

        return ingredientsFromSpecifiedRecipe;
    }
    public void DisplayingRecipes()
    {
        try
        {
            if (!Recipes.Any())
                throw new ArgumentNullException("List of recipes is still empty");

            foreach (var recipe in Recipes)
            {
                Console.WriteLine(@$"{recipe.Id}. {recipe.Name}");
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ParamName);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
    public async Task<List<IngredientClass>> GetIngredients(string fileName)
    {
        try
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var jsonFilePath = Path.Combine(basePath, @$"..\..\..\FilesWithContentJSON\{fileName}");
            var jsonString = File.ReadAllTextAsync(jsonFilePath);
            return JsonSerializer.Deserialize<List<IngredientClass>>(await jsonString);
        }
        catch (FileNotFoundException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            Console.ReadKey();
        }
        return null;
    }
    public async Task<List<RecipeClass>> GetRecipes(string fileName)
    {
        try
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var jsonFilePath = Path.Combine(basePath, $@"..\..\..\FilesWithContentJSON\{fileName}");
            var jsonString = File.ReadAllTextAsync(jsonFilePath);
            return JsonSerializer.Deserialize<List<RecipeClass>>(await jsonString);
        }
        catch (FileNotFoundException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            Console.ReadKey();
        }
        return null;
    }
}