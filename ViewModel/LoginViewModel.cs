using System.Windows;

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
