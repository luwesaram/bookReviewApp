using bookReviewConsoleApplication.ViewModel;
using System.Windows;

namespace bookReviewConsoleApplication
{
    public partial class RegisterWindow : Window
    {
        Connection Conn = new Connection();
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnCreateAcc_Click(object sender, RoutedEventArgs e)
        {
            string username = txtBoxUsername.Text;
            string email = txtBoxEmail.Text;
            string password = psBoxPass.Password;
            string confirm = psBoxConfirmPass.Password;

            RegisterViewModel registerModel = new RegisterViewModel(username, email, password, confirm, this);
        }

        private void btnLoginNow_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}



