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

Console.ReadKey();
