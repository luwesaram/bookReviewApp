using bookReviewConsoleApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookReviewConsoleApplication.ViewModel
{
    public class ReviewViewModel
    {
        public List<int> Ratings { get; set; } = new List<int> { 5, 4, 3, 2, 1 };
        public int SelectedRating { get; set; }
        public ReviewViewModel(Book book)
        {
            Book = book;
            SelectedRating = 5;
        }

        private Book _book;
        public Book Book
        {
            get { return _book; }
            set
            {
                _book = value;
                OnPropertyChanged(nameof(Book));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}