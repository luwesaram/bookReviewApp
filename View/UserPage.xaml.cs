using bookReviewConsoleApplication.Model;
using bookReviewConsoleApplication.View;
using bookReviewConsoleApplication.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookReviewConsoleApplication
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public UserPage()
        {
            InitializeComponent();
            MessageBox.Show("Hi", "Nice", MessageBoxButton.OK);
            this.DataContext = new MainViewModel();
        }

        private void ImgMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = (Image)sender;

            if (image.Tag is Book book)
            {
                BookDetailPage bookDetailPage = new BookDetailPage(book);
                bookDetailPage.Show();
                this.Close();
            }

        }
    }
}
