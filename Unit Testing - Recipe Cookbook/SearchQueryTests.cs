using NUnit.Framework;
using Stage_1___Recipe_Searching;

namespace Unit_Testing___Recipe_Cookbook
{
 
    public class SearchQueryTests
    {
        private SearchQuery search = new SearchQuery();
        
        [Test]
        public void InputChoice_WithNoInput_ShouldReturnInvalidOperationException()
        {

            string userInput = "";

            Assert.Throws<InvalidOperationException>(() => search.InputChoice(userInput));

        }

        [Test]
        public void DisplayingIngredients_WithEmptyLists_ReturnInvalidOperationException()
        {

            search.Ingredients = null;

            Assert.Throws<InvalidOperationException>(() => search.DisplayingIngredients() );


        }

        [Test]
        public void DisplayingRecipes_WithEmptyLists_ReturnInvalidOperationException()
        {

            search.Recipes = null;

            Assert.Throws<InvalidOperationException>(() => search.DisplayingIngredients());

        }

        //[Test]
        //public void GetRecipes_WithInvalidName_ShouldReturnFileNotFoundException()
        //{
        //    string fileName = "";


        //    Assert.ThrowsAsync(() => search.GetIngredients(fileName));
            
        //}

    }
}
