using bookReviewConsoleApplication.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace bookReviewConsoleApplication.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly Connection connection;
        private readonly BookManager bookManager;
        
        // when MainViewModel loads, initializes a connection and an bookmanager object
        // afterwards, it loads the data necesssary in order to fill the reviews and books with the data needed for the View
        
        public MainViewModel() 
        {
            connection = new Connection();
            bookManager = new BookManager(connection);
            LoadDataAsync();
        }

        private ObservableCollection<Book> books { get; set; }
        public ObservableCollection<Book> Books { 
            get { return books; } 
            set {
                books = value;
                OnPropertyChanged(nameof(Books));
            } 
        }

        // data is loaded asyncly in order to not block the other tasks. 
        private async void LoadDataAsync() 
        {
            Books = await bookManager.GetMostRecentBooks();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}