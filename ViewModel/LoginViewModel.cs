using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bookReviewConsoleApplication.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel(string username, string password, Window mainWindow)
        {
            loginMethod(username, password, mainWindow);
        }

        private void loginMethod(string username, string password, Window currentWindow)
        {
            UserManager userLogin = new UserManager(username, password, currentWindow);
        }
    }
}
