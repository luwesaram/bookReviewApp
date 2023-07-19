using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Windows;
using System.Windows.Input;

namespace bookReviewConsoleApplication
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
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

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your username, email, and password!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (confirm != password)
            {
                MessageBox.Show("Passwords do not match!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if(UserExists(username, email))
            {
                MessageBox.Show("User already Exists", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Registered Successfully", "Registration Success", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public bool UserExists(string username, string email)
        {
            bool exists = false;

            try
            {
                if (Conn.OpenConnection())
                {
                    string sql = "SELECT COUNT(*) FROM user WHERE Username = @username OR Email = @email";
                    MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection());
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    exists = count > 0;

                }
                else
                {
                    // Handle the case when the database connection fails
                    MessageBox.Show("Unable to connect to the database.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error: " + error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Conn.CloseConnection();
            return exists;
        }

        private void lblExistUserDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}



