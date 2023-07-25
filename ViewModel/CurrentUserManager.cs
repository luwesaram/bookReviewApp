using bookReviewConsoleApplication.Model;

namespace bookReviewConsoleApplication.ViewModel
{
    public class CurrentUserManager
    {
        private static CurrentUserManager instance;
        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }


        public static CurrentUserManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentUserManager();
                }
                return instance;
            }
        }

        private CurrentUserManager()
        {

        }
    }
}
