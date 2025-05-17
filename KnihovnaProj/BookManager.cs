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

    }

    
}