using bookReviewConsoleApplication.Entities.Interface;

namespace bookReviewConsoleApplication.Model
{
    public class User : IUser
    {
        public User()
        {

        }
        public User(string username)
        {
            this.Username = username;
        }
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
