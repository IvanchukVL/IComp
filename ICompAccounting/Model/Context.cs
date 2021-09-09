using ICompAccounting.Model.Entities;
using ICompAccounting.Model.Entities.oper;
using ICompAccounting.Model.Entities.org;
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
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountPurpose> AccountPurposes { get; set; }
        public DbSet<vAccountsPurposes> vAccountsPurposes { get; set; }
        public DbSet<vPurposes> vPurposes { get; set; }
        public DbSet<Operation> OperationList { get; set; }
        public DbSet<UsersLocalParam> UsersLocalParams { get; set; }
        public DbSet<OperationOut> OperationsOut { get; set; }
        public DbSet<vPartnersAccount> vPartnersAccounts { get; set; }


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
            modelBuilder.Entity<UsersLocalParam>().HasKey(o => new { o.UserId, o.EnterpriseId });
            base.OnModelCreating(modelBuilder);

        }
    }

    public class OrganizationsContext : DbContext
    {
        
        public DbSet<Partner> BD_ORG { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Operation> OperationList { get; set; }

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
