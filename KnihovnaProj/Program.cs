using System.Globalization;
using KnihovnaProj;

namespace KnihovnaProj;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Zadej:");
        Console.WriteLine(" - ADD;title;author;publishedDate in format YYYY-MM-DD;number of pages - to add abook");
        Console.WriteLine(" - LIST to list all books in your list");
        Console.WriteLine(" - STATS for statistics");
        Console.WriteLine(" - FIND;word to find a key word");
        Console.WriteLine(" - END to finish");
        List<Book> books = new List<Book>();
        Dictionary<DateTime, int> stats = new Dictionary<DateTime, int>();

        while (true)
        {

            System.Console.WriteLine("Zadej příkaz (ADD, LIST, STATS, FIND, END)");
            string entry = Console.ReadLine();

            //end program
            if (entry.Trim().ToUpper() == "END") break;

            try
            {

                //add a book
                if (entry.Trim().ToUpper().StartsWith("ADD"))
                {

                    string[] entrySplitted = entry.Split(";");
                    // format check
                    if (entrySplitted.Length != 5)
                    {
                        Console.WriteLine("Wrong format, please enter this format: ADD;title;author;publishedDate in format YYYY-MM-DD;number of pages - to add a book");
                        continue;
                    }
                    //Parse the user input
                    string title = entrySplitted[1].Trim();
                    string author = entrySplitted[2].Trim();
                    string entryDate = entrySplitted[3].Trim();
                    int numberOfPages = 0;
                    if (!int.TryParse(entrySplitted[4].Trim(), out numberOfPages))
                    {
                        Console.WriteLine("Invalid format for number of pages. Please enter a whole number.");
                        continue;
                    }

                    //Parse the date and create datetime entry
                    if (DateTime.TryParseExact(entryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publishedDate))
                    {
                        //Create a book
                        Book newBook = new Book(title, author, publishedDate, numberOfPages);
                        books.Add(newBook);
                        Console.WriteLine("------- Book added successfully -------");
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Use YYYY-MM-DD (e.g., 2025-05-17).");
                    }


                }
                else if (entry.Trim().ToUpper() == "LIST")
                {
                    //If there are not any books, warn the user
                    if (BookManager.CheckIfBookListIsEmpty(books)) continue;

                    System.Console.WriteLine("------- Book List -------");
                    //List the books based on publishedtime
                    BookManager.PrintAllBooks(books);
                }
                else if (entry.Trim().ToUpper() == "STATS")
                {

                    //If there are not any books, warn the user
                    if (BookManager.CheckIfBookListIsEmpty(books)) continue;


                    int averageNumberOfPages = (int)Math.Round(books.Select(e => e.Pages).Average());
                    var countOfBooksPerAuthor = books.GroupBy(a => a.Author).Select(g => new { Author = g.Key, Count = g.Count() });
                    string uniqueWords = string.Join(", ", books.SelectMany(b => b.Title.Split(" ")).Select(w => w.ToLower()).Distinct());

                    System.Console.WriteLine("------- Statistics -------");
                    System.Console.WriteLine($"Average number of pages is {averageNumberOfPages}");
                    System.Console.WriteLine("Number of books per author:");

                    foreach (var group in countOfBooksPerAuthor)
                    {
                        System.Console.WriteLine($"{group.Author}: {group.Count} book(s)");
                    }


                    System.Console.WriteLine($"Unique words are: {uniqueWords}");

                }
                else if (entry.Trim().ToUpper().StartsWith("FIND"))
                {
                    //If there are not any books, warn the user
                    if (BookManager.CheckIfBookListIsEmpty(books)) continue;


                    string[] wordSplitted = entry.Split(";");

                    // check the entered format
                    if (wordSplitted.Length != 2)
                    {
                        Console.WriteLine("Please enter: FIND;keyword");
                        continue;
                    }

                    string wordToLookfor = wordSplitted[1].Trim();
                    var foundBooks = books.Where(b => b.Title.ToLower().Contains(wordToLookfor.ToLower()));

                    if (!foundBooks.Any())
                    {
                        Console.WriteLine("No books found containing that keyword.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("------- Found books: -------");
                        foreach (var book in foundBooks)
                        {
                            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wrong format, please enter this format: ADD;title;author;publishedDate in format YYYY-MM-DD;number of pages - to add a book");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected mistake, please try again.");
            }
        }


    }
}
