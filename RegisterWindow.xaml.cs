using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Windows;
using System.Windows.Input;

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

            try {

                if(!Conn.OpenConnection()) 
                {
                    MessageBox.Show("Unable to connect to the database.", "Error");
                    return;
                }

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
                    MessageBox.Show("I am here");
                    if(CreateUser(username, email, password))
                    {
                        // redirect to another window
                    }
                    else 
                    {
                        MessageBox.Show("Failed to create user");
                    }

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
        }

        public bool UserExists(string username, string email)
        {
            bool exists = false;

            try
            {
                string sql = "SELECT COUNT(*) FROM user WHERE Username = @username OR Email = @email";
                MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection());
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@email", email);

                int count = Convert.ToInt32(command.ExecuteScalar());
                exists = count > 0;
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error: " + error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return exists;
        }

        public bool CreateUser(string username, string email, string password)
        {
            try
            { 
                string sql = $"INSERT INTO user (Username, Email, Password) VALUES ('{username}', '{email}', '{password}')";
                MySqlCommand command = new MySqlCommand(sql, Conn.GetConnection());

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error: " + error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }

        private void lblExistUserDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}



