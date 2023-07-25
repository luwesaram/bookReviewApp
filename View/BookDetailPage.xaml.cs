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
        private readonly BookDetailViewModel viewModel;
        private Book currentBook;

        public BookDetailPage(Book book)
        {
            InitializeComponent();

            viewModel = new BookDetailViewModel(book);
            currentBook = book;

            DataContext = viewModel;

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

    }
}
