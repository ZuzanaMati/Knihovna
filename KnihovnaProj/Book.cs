using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnihovnaProj
{
    public class Book
    {
        public string Title;
        public string Author;
        public DateTime PublishedDate;
        public int Pages
        {
            get; set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number of pages cannot be negative.");
                }
                else
                {
                    field = value;
                }
            }
        }

        public Book(string title, string author, DateTime publishedDate, int pages)
        {
            this.Title = title;
            this.Author = author;
            this.PublishedDate = publishedDate;
            this.Pages = pages;
        }


    }
}