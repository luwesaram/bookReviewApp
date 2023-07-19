using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Input;

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

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) 
            {
                MessageBox.Show("Username/Password cannot be empty", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try 
            {
                if(!Conn.OpenConnection())
                {
                    MessageBox.Show("Cannot connect to the database", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(isValidLogin(username, password))
                {
                    MessageBox.Show("Login Successfully", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                    // insert code to redirect to main

                    UserPage userPage = new UserPage();
                    userPage.Show();
                    this.Close();
                } 
                else 
                {
                    MessageBox.Show("Login Error. Check your username and password and try again.", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } 
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conn.CloseConnection();
            }

            txtBxUsername.Text = "";
            txtBxPassword.Password = "";
        }

        private bool isValidLogin(string username, string password) 
        {
            object Result = null;

            try
            {
                string sql = "SELECT Username FROM user WHERE Username = '"+ username +"' AND Password = '"+ password +"'";
                MySqlCommand Statement = new MySqlCommand(sql, Conn.GetConnection());
                Result = Statement.ExecuteScalar();

                Conn.CloseConnection();
            }
            catch (MySqlException Error)
            {
                MessageBox.Show("Error: " + Error, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                
            return Result != null;
        }

        private void lblForgotPassDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblCreateAccDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
