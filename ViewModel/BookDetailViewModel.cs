using System.ComponentModel;
using bookReviewConsoleApplication.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace bookReviewConsoleApplication.ViewModel
{
    public class BookDetailViewModel : INotifyPropertyChanged
    {
        private readonly Connection connection;
        private readonly ReviewManager reviewManager;
        public bool IsAlreadyReviewed;

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

        private ObservableCollection<Review> _reviews;

        public ObservableCollection<Review> Reviews
        {
            get { return _reviews; }
            set
            {
                _reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }

        public BookDetailViewModel(Book book)
        {
            Book = book;
            connection = new Connection();
            reviewManager = new ReviewManager(connection);
            Reviews = new ObservableCollection<Review>();
            IsAlreadyReviewed = reviewManager.IsReviewed(book);
            LoadReviewsAsync();
        }

        private async void LoadReviewsAsync()
        {
            var recentReviews = await reviewManager.GetAllReviews(Book);

            if (recentReviews != null)
            {
                foreach (var review in recentReviews)
                {
                    Reviews.Add(review);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
