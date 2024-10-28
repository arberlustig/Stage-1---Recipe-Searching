namespace Stage_1___Recipe_Searching.Interfaces;

public interface ISearchQuery
{
    public Task<List<IngredientClass>> GetIngredients(string fileName);
    public Task<List<RecipeClass>> GetRecipes(string fileName);
    void DisplayingRecipes();
    void DisplayingIngredients();
    public void InputChoice(string userInput);
}