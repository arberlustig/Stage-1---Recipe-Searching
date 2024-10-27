namespace Stage_1___Recipe_Searching.Interfaces;

public interface ISearchQuery
{
    public Task<List<IngredientClass>> GetIngredients();
    public Task<List<RecipeClass>> GetRecipes();
    public void DisplayRecipes();
}