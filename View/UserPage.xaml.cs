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

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var currentWindow = Window.GetWindow(this);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                currentWindow.Close();
            }
        }
    }
}
