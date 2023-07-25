using bookReviewConsoleApplication.ViewModel;
using System.Windows;

namespace bookReviewConsoleApplication
{

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtBxUsername.Text;
            string password = txtBxPassword.Password;
            LoginViewModel loginModel = new(username, password, this);
        }

        private void btnCreateNow_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();
            this.Close();
        }
    }
}
