using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace bookReviewConsoleApplication.View
{
    public partial class AddReviewWindow : Window
    {
        private readonly ReviewViewModel viewModel;
        private readonly ReviewManager reviewManager;
        private readonly Connection Conn;
        private Book currentBook;
        public List<int> Ratings { get; set; } = new List<int> { 5, 4, 3, 2, 1 };
        public int SelectedRating { get; set; }

        public AddReviewWindow(Book book)
        {
            InitializeComponent();

            User user = CurrentUserManager.Instance.CurrentUser;
            Conn = new Connection();
            reviewManager = new ReviewManager(Conn);
            currentBook = book;
            
            viewModel = new ReviewViewModel(book);
            DataContext = viewModel;

            lblUserName.Content = "Hi " + user.Username;
            SelectedRating = 5;
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
            int rating = viewModel.SelectedRating;

            MessageBox.Show(" " + rating, "Status", MessageBoxButton.OK);

            if (!string.IsNullOrEmpty(description))
            {
                reviewManager.AddReview(description, rating, currentBook);
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
