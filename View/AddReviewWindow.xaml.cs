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
        private readonly Book currentBook;

        public AddReviewWindow(Book book)
        {
            InitializeComponent();

            Conn = new Connection();
            reviewManager = new ReviewManager(Conn);
            currentBook = book;
            
            viewModel = new ReviewViewModel(book);
            DataContext = viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            BookDetailPage bookDetail = new(currentBook);
            bookDetail.Show();
            this.Close();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string description = TxtBxReview.Text;
            int rating = viewModel.SelectedRating;

            if (!string.IsNullOrEmpty(description))
            {
                reviewManager.AddReview(description, rating, currentBook);
                BookDetailPage bookDetailPage = new(currentBook);
                bookDetailPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Fields must not be empty", "Empty Fields", MessageBoxButton.OK);
            }
        }
    }
}
