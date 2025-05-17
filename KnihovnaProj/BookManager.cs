using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnihovnaProj
{
    public class BookManager
    {

        public static bool CheckIfBookListIsEmpty(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("You do not have any books added.");
                return true;
            }

            return false;
        }

        public static void PrintAllBooks(List<Book> books)
        {

            foreach (var book in books.OrderBy(b => b.PublishedDate))
            {
                Console.WriteLine($"Book: {book.Title}, Author: {book.Author}, Published: {book.PublishedDate:dd.MM.yyyy}, Pages: {book.Pages}");
            }
        }

        public static void PrintStats(List<Book> books)
        {
            int averageNumberOfPages = (int)Math.Round(books.Select(b => b.Pages).Average());

            var countOfBooksPerAuthor = books
                .GroupBy(b => b.Author)
                .Select(g => new { Author = g.Key, Count = g.Count() });

            string uniqueWords = string.Join(", ",
                books
                    .SelectMany(b => b.Title.Split(" "))
                    .Select(w => w.ToLower())
                    .Distinct()
            );

            System.Console.WriteLine("------- Statistics -------");
            System.Console.WriteLine($"Average number of pages is {averageNumberOfPages}");
            System.Console.WriteLine("Number of books per author:");
            foreach (var group in countOfBooksPerAuthor)
            {
                System.Console.WriteLine($"{group.Author}: {group.Count} book(s)");
            }

            System.Console.WriteLine($"Unique words are: {uniqueWords}");
        }

        public static void FindBooks(List<Book> books, string keyword)
        {
            var foundBooks = books
                .Where(b => b.Title.ToLower().Contains(keyword.ToLower()));

            if (!foundBooks.Any())
            {
                System.Console.WriteLine("No books found containing that keyword.");
                return;
            }

            System.Console.WriteLine("------- Found books: -------");
            foreach (var book in foundBooks)
            {
                System.Console.WriteLine($"Title: {book.Title}, Author: {book.Author}");
            }
        }

    }
    
}