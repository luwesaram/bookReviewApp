using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.ViewModel;
using System.Windows;

namespace bookReviewConsoleApplication.View
{
    public partial class AddReviewWindow : Window
    {
        private readonly BookDetailViewModel viewModel;
        private Book currentBook;

        public AddReviewWindow(Book book)
        {
            InitializeComponent();
            User user = CurrentUserManager.Instance.CurrentUser;
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
