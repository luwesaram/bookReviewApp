using System.Windows;

namespace bookReviewConsoleApplication.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel(string username, string password, Window mainWindow)
        {
            UserManager userLogin = new(username, password, mainWindow);
        }
    }
}
