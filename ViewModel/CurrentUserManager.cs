using System;
using bookReviewConsoleApplication.Model;

namespace bookReviewConsoleApplication.ViewModel
{
    public class CurrentUserManager
    {
        private static CurrentUserManager instance;
        private User _currentUser;

        private Author? _author;
        public Author? Author
        {
            get { return _author; }
            set { _author = value; }
        }
        

        public bool IsAuthor() => GetAuthor() != null;
        private Author? GetAuthor()
        {
            UserManager userManager = new();
            if(userManager.IsAuthor()) {
                Author = userManager.GetAuthor();
            }

            return Author;
        }

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
