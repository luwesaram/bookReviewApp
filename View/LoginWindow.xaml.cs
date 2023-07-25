using bookReviewConsoleApplication.ViewModel;
using System.Windows;

namespace bookReviewConsoleApplication
{

    public partial class LoginWindow : Window
    {
        Connection Conn = new Connection();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtBxUsername.Text;
            string password = txtBxPassword.Password;
            LoginViewModel loginModel = new LoginViewModel(username, password, this);
        }

        private void btnCreateNow_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
