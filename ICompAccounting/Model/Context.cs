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

    public class OrganizationsContext : DbContext
    {
        public DbSet<BD_ORG> Organizations { get; set; }
        public DbSet<BD_RAH> Accounts { get; set; }

        public OrganizationsContext() : base()
        {
            Database.EnsureCreated();
        }
        public OrganizationsContext(DbContextOptions<OrganizationsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var coonectionString = "Data Source=IT_PROGR_04V\\DRUID;Initial Catalog=BD_ORG;Persist Security Info=True;User ID=AccountingRobot;Password=Asteriks77;Timeout=300";
        //    //optionsBuilder.UseSqlServer(coonectionString, builder => builder.UseRowNumberForPaging());
        //    //optionsBuilder.UseSqlServer(coonectionString);
        //}
    }

}
