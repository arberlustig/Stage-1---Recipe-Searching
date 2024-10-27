SearchQuery search = new SearchQuery();
search.Initialize();

Console.WriteLine("What do you want to do?");
Console.WriteLine(@"
[L]ooking at all recipes!
[R]etrieve the ingredients for a specified recipe!
[S]earch for recipes that contain specified ingredients!
");

string userInput = Console.ReadLine();
search.InputChoice(userInput);








// users can retrieve the ingredients for a specified recipe
//-> ausgeben der ganzen RecipesNames
//-> mithilfe einer ID kann er die Ingredients rauslesen
//-> darauf basierend mit ID 

Console.ReadKey();
