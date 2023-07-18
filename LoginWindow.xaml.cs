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

            //check if username input and pass is empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) 
            {
                MessageBox.Show("Username/Password cannot be empty", "Error!");
            }
            //proceed if not empty
            else
            {
                //sql statement
                string sql = "SELECT Username FROM user WHERE Username = '"+ username +"' AND Password = '"+ password +"'";
                //check if connection is established
                if(Conn.OpenConnection() == true)
                {
                    try
                    {
                        MySqlCommand Statement = new MySqlCommand(sql, Conn.GetConnection());
                        object Result = Statement.ExecuteScalar();
                        if (Result == null)
                        {
                            MessageBox.Show("Invalid username/password.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Successfully logged in.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (MySqlException Error)
                    {
                        MessageBox.Show("Error: " + Error, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            txtBxUsername.Text = "";
            txtBxPassword.Password = "";
            Conn.CloseConnection();
        }

        /* private bool isValidLogin(string username, string password) 
        {

        }*/

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
