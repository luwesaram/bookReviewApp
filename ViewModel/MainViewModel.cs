using System.Collections.ObjectModel;
using System.ComponentModel;
using bookReviewConsoleApplication.Model;

namespace bookReviewConsoleApplication.ViewModel 
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly Connection connection;
        private readonly BookManager bookManager;
        // when MainViewModel loads, initializes a connection and an bookmanager object
        // afterwards, it loads the data necesssary
        public MainViewModel() 
        {
            connection = new Connection();
            bookManager = new BookManager(connection);

            LoadData();
        }

        private ObservableCollection<Book> books { get; set; }
        public ObservableCollection<Book> Books { 
            get { return books; } 
            set {
                books = value;
                OnPropertyChanged(nameof(Books));
            } 
        }

        private ObservableCollection<Review> reviews;

        public ObservableCollection<Review> Reviews
        {
            get { return reviews; }
            set { 
                reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }
        
        private async void LoadData() 
        {
            Books = new ObservableCollection<Book>(await bookManager.GetMostRecentBooks(5));
            Reviews = new ObservableCollection<Review>(await bookManager.GetMostRecentReviews(5));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}