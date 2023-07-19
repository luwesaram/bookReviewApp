using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookReviewConsoleApplication.Entities.Interface
{
    public interface IUser
    {
        int Id { get; set; } 
        string Email { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
