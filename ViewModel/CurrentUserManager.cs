using System;
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

        private Author? _author;
        public Author? Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public void Initialize(User currentUser)
        {
            _currentUser = currentUser;
            UserManager userManager = new();
            if (userManager.IsAuthor())
            {
                Author = userManager.GetAuthor();
            }
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
