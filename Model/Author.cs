using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookReviewConsoleApplication.Model
{
    public class Author : User
    {
        public Author(string username) : base(username) { }
        
        public string PenName { get; set; }
    }
}
