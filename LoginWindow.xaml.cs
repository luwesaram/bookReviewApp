using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bookReviewConsoleApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
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
            //.Show(username, password);
            // Connect to database table
            // Check if username and password credentials match 
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
