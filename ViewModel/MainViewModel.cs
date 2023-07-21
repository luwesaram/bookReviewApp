using System.Collections.ObjectModel;
using System.ComponentModel;
using bookReviewConsoleApplication.Model;

namespace bookReviewConsoleApplication.ViewModel 
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel() 
        {

        }

        private ObservableCollection<Book> books { get; set; }
        public ObservableCollection<Book> Books { 
            get { return books; } 
            set {
                books = value;
                onPropertyChanged(nameof(Books))
            } 
        }

        private ObservableCollection<Review> reviews;

        public ObservableCollection<Review> Reviews
        {
            get { return reviews; }
            set { reviews = value; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void onPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}