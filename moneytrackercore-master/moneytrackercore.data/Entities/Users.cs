﻿using System;
using System.Collections.Generic;
using System.Text;

namespace moneytrackercore.data.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Balance Balance { get; set; }



    }
}
