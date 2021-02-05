using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingContext.dl
{
    public class AccountingContext : DbContext
    {
        public DbSet<vmenu> vMenu { get; set; }
        public DbSet<menu> Menu { get; set; }
        public DbSet<User> Users { get; set; }

        public AccountingContext() : base()
        {
            Database.EnsureCreated();
        }
        public AccountingContext(DbContextOptions<AccountingContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vmenu>().HasKey(o => new { o.UserId, o.MenuId });
            base.OnModelCreating(modelBuilder);

        }
    }
}
