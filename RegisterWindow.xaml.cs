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

            else
            {

                if (!UserExists(username, email))
                {
                    
                    if(Conn.OpenConnection())
                    {
                        //checks if connection is established
                        try
                        {
                            // creates a user
                            string sql = "INSERT INTO user (Username, Email, Password) VALUES ('" + username + "','" + email + "','" + password + "')";
                            MySqlCommand Statement = new MySqlCommand(sql, Conn.GetConnection());
                            object Result = Statement.ExecuteScalar();
                            MessageBox.Show("Creating User.", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        
                        catch (MySqlException Error)
                        {
                            // catches error
                            MessageBox.Show("Error: " + Error, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    // error when user already exists
                    MessageBox.Show("User already Exists", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                txtBoxUsername.Text = "";
                txtBoxEmail.Text = "";
                psBoxPass.Password = "";
                psBoxConfirmPass.Password = "";
                Conn.CloseConnection();
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

                    Conn.CloseConnection();
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



