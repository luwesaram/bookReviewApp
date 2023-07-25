using System.ComponentModel;
using bookReviewConsoleApplication.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace bookReviewConsoleApplication.ViewModel
{
    public class BookDetailViewModel : INotifyPropertyChanged
    {
        private readonly Connection connection;
        private readonly ReviewManager reviewManager;
        public bool IsAlreadyReviewed { get; set; }

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

        private int oneStarCount;
        public int OneStarCount
        {
            get { return oneStarCount; }
            set
            {
                if (oneStarCount != value)
                {
                    oneStarCount = value;
                    OnPropertyChanged(nameof(OneStarCount));
                }
            }
        }

        private int twoStarCount;
        public int TwoStarCount
        {
            get { return twoStarCount; }
            set
            {
                if (twoStarCount != value)
                {
                    twoStarCount = value;
                    OnPropertyChanged(nameof(OneStarCount));
                }
            }
        }

        private int threeStarCount;
        public int ThreeStarCount
        {
            get { return threeStarCount; }
            set
            {
                if (threeStarCount != value)
                {
                    threeStarCount = value;
                    OnPropertyChanged(nameof(ThreeStarCount));
                }
            }
        }

        private int fourStarCount;
        public int FourStarCount
        {
            get { return fourStarCount; }
            set
            {
                if (fourStarCount != value)
                {
                    fourStarCount = value;
                    OnPropertyChanged(nameof(FourStarCount));
                }
            }
        }

        private int fiveStarCount;
        public int FiveStarCount
        {
            get { return fiveStarCount; }
            set
            {
                if (fiveStarCount != value)
                {
                    fiveStarCount = value;
                    OnPropertyChanged(nameof(FiveStarCount));
                }
            }
        }

        private int allStarCount;
        public int AllStarCount
        {
            get { return allStarCount; }
            set
            {
                if (allStarCount != value)
                {
                    allStarCount = value;
                    OnPropertyChanged(nameof(AllStarCount));
                }
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
            OneStarCount = reviewManager.OneStar(book);
            TwoStarCount = reviewManager.TwoStar(book);
            ThreeStarCount = reviewManager.ThreeStar(book);
            FourStarCount = reviewManager.FourStar(book);
            FiveStarCount = reviewManager.FiveStar(book);
            AllStarCount = reviewManager.AllStar(book);
            Console.WriteLine("IsAlreadyReviewed " + IsAlreadyReviewed);
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

        public void ReviewCount(Book book)
        {
            OneStarCount = reviewManager.OneStar(book);
            TwoStarCount = reviewManager.TwoStar(book);
            ThreeStarCount = reviewManager.ThreeStar(book);
            FourStarCount = reviewManager.FourStar(book);
            FiveStarCount = reviewManager.FiveStar(book);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
