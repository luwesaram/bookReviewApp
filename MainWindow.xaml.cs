using System.Windows;

namespace bookReviewConsoleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoginClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
