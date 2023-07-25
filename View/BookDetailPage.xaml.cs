using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.ViewModel;
using System.Windows;

namespace bookReviewConsoleApplication.View
{
    /// <summary>
    /// Interaction logic for BookDetailPage.xaml
    /// </summary>
    public partial class BookDetailPage : Window
    {
        private readonly ReviewDetailViewModel viewModel;
        private Book currentBook;

        public BookDetailPage(Book book)
        {
            InitializeComponent();
            User currentUser = CurrentUserManager.Instance.CurrentUser;

            viewModel = new ReviewDetailViewModel(book);
            currentBook = book;

            DataContext = viewModel;
            lblUserName.Content = "Hi " + currentUser.Username;

            if (viewModel.IsAlreadyReviewed)
            {
                BtnCreate.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnCreate.Visibility = Visibility.Visible;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            UserPage userPage = new UserPage();
            userPage.Show();
            this.Close();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            AddReviewWindow addReview = new AddReviewWindow(currentBook);
            addReview.Show();
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
