using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(): base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }

    }
}