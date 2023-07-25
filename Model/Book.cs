using System;
using System.Windows.Media;

namespace bookReviewConsoleApplication.Model
{
    public class Book
    {
        public string ISBNNumber { get; set; }
        public Author Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public ImageSource CoverImage { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PageCount { get; set; }
    }
}
