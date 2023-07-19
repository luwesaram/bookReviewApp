using bookReviewConsoleApplication.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookReviewConsoleApplication.Entities
{
    public class Author : User
    {
        public string PenName { get; set; }
    }
}
