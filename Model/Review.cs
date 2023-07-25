using System;

namespace bookReviewConsoleApplication.Model
{
    public class Review
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
