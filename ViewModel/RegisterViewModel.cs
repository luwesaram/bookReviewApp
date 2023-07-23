using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bookReviewConsoleApplication.ViewModel
{
    public class RegisterViewModel
    {

        public RegisterViewModel(string username, string email, string password, string confirm, Window mainWindow)
        {
            RegisterMethod(username, email, password, confirm, mainWindow);
        }

        public void RegisterMethod(string username, string email, string password, string confirm, Window currentWindow)
        {
            UserManager registerUser = new UserManager(username,email,password,confirm,currentWindow);
        }
    }
}
