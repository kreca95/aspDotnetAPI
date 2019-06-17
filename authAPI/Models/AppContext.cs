using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace authAPI.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("AppContext")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }


    }
}
