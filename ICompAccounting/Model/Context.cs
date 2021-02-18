using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICompAccounting.Model
{
    public class AccountingContext : DbContext
    {
        public DbSet<vmenu> vMenu { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<vReferenceValue> vReferenceValues { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<vUser> vUsers { get; set; }

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
