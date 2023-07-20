﻿using bookReviewConsoleApplication.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookReviewConsoleApplication.Model
{
    public class User : IUser
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
