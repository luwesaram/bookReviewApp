using bookReviewConsoleApplication.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bookReviewConsoleApplication.ViewModel
{
    public class UserManager
    {
        private Connection Conn = new Connection();
        
        public UserManager(string username, string password, Window mainWindow)
        {
            LoginUser(username, password, mainWindow);  
        }

        public UserManager(string username, string email, string password, string confirm, Window mainWindow)
        {
            RegisterUser(username, email, password, confirm, mainWindow);
        }

        // Register Methods
        public void RegisterUser(string username, string email, string password, string confirm, Window currentWindow)
        {
            try
            {

                if (!Conn.OpenConnection())
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

                else if (UserExists(username, email))
                {
                    MessageBox.Show("User already Exists", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("User created successfully!", "SUCCESS!", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (CreateUser(username, email, password))
                    {
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        currentWindow.Close();
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
        
        //check if username/email already exists when an account is registered
        public bool UserExists(string username, string email)
        {
            bool exists = false;

            try
            {
                string sql = "SELECT COUNT(*) FROM user WHERE username = @username OR email = @email";
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

        // create user command
        public bool CreateUser(string username, string email, string password)
        {
            try
            {
                string sql = $"INSERT INTO user (username, email, password) VALUES ('{username}', '{email}', '{password}')";
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

        // Login Methods
        private User? IsValidLogin(string username, string password)
        {
            User? user = null;

            try
            {
                string sql = "SELECT id, username FROM user WHERE username = '" + username + "' AND Password = '" + password + "'";
                MySqlCommand Statement = new MySqlCommand(sql, Conn.GetConnection());

                using (MySqlDataReader reader = Statement.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        user = new User
                        {
                            Id = reader.GetInt32("id"),
                            Username = reader.GetString("username")
                        };  
                    }
                }

                Conn.CloseConnection();
            }
            catch (MySqlException Error)
            {
                MessageBox.Show("Error: " + Error, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return user;
        }

        private void LoginUser(string username, string password, Window currentWindow)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username/Password cannot be empty", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                if (!Conn.OpenConnection())
                {
                    MessageBox.Show("Cannot connect to the database", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                User? user = IsValidLogin(username, password); 

                if (user != null)
                {
                    MessageBox.Show("Login Successfully", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                    // insert code to redirect to main
                    UserPage userPage = new UserPage(user);
                    userPage.Show();
                    currentWindow.Close();
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
        }
    }
}
