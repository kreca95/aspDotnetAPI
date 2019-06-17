﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
    }
}