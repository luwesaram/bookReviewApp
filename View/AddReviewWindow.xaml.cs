using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.ViewModel;
using System.Windows;

namespace bookReviewConsoleApplication.View
{
    public partial class AddReviewWindow : Window
    {
        private readonly BookDetailViewModel viewModel;
        private readonly ReviewManager reviewManager;
        private readonly Connection Conn;
        private Book currentBook;

        public AddReviewWindow(Book book)
        {
            InitializeComponent();
  
            Conn = new Connection();
            User user = CurrentUserManager.Instance.CurrentUser;
            reviewManager = new ReviewManager(Conn);
            viewModel = new BookDetailViewModel(book);
            currentBook = book;
            DataContext = viewModel;
            lblUserName.Content = "Hi " + user.Username;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            BookDetailPage bookDetail = new BookDetailPage(currentBook);
            bookDetail.Show();
            this.Close();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string description = TxtBxReview.Text;

            if(!string.IsNullOrEmpty(description))
            {
                reviewManager.AddReview(description, currentBook);
                BookDetailPage bookDetailPage = new BookDetailPage(currentBook);
                bookDetailPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Fields must not be empty", "Empty Fields", MessageBoxButton.OK);
            }


        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                CurrentUserManager.Instance.CurrentUser = null;
                var currentWindow = Window.GetWindow(this);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                currentWindow.Close();
            }
        }
    }
}
