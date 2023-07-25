using System.Windows;

namespace bookReviewConsoleApplication.ViewModel
{
    public class RegisterViewModel
    {

        public RegisterViewModel(string username, string email, string password, string confirm, Window mainWindow)
        {
            UserManager registerUser = new(username, email, password, confirm, mainWindow);
        }
    }
}
