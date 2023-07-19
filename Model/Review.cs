using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace bookReviewConsoleApplication.Entities
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
