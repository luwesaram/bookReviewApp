namespace bookReviewConsoleApplication.Model
{
    public class Author : User
    {
        public Author(string username) : base(username) { }

        public string PenName { get; set; }
    }
}
