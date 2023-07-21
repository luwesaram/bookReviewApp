using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Threading.Tasks;
using bookReviewConsoleApplication.Model;
using System;

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
        
        // data is loaded asyncly in order to not block the other tasks. 
        private async void LoadData() 
        {
            var booksTask = bookManager.GetMostRecentBooks(5);
            var reviewsTask = bookManager.GetMostRecentReviews(5);

            try
            {
                await Task.WhenAll(booksTask, reviewsTask);
                // Ensure the current dispatcher is valid and update the UI properties.
                if (Application.Current != null && Application.Current.Dispatcher != null)
                {
                    Application.Current.Dispatcher.Invoke(() => {
                        Books = new ObservableCollection<Book>(booksTask.Result);
                        Reviews = new ObservableCollection<Review>(reviewsTask.Result);
                    });
                }
                else
                {
                    // If dispatcher is null, handle the situation appropriately.
                    MessageBox.Show("Unable to update UI. Dispatcher is null.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, if needed.
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}