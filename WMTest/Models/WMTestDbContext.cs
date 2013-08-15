using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WMTest.Models
{
    public class WMTestDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}