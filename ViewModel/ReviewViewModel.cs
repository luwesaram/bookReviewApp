using bookReviewConsoleApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookReviewConsoleApplication.ViewModel
{
    public class ReviewViewModel
    {
        public List<int> Ratings { get; set; } = new List<int> { 5, 4, 3, 2, 1 };
        public int SelectedRating { get; set; }
        public Book currentBook;
        public ReviewViewModel(Book book)
        {
            currentBook = book;            
        }
    }
}
