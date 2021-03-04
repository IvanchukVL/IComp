//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ICompAccounting.Model
{
    public class RepositoryOrg:IDisposable
    {
        public DbContextOptionsBuilder<OrganizationsContext> OptionsBuilder { set; get; }
        public OrganizationsContext db;

        public RepositoryOrg(string ConnectionString)
        {
            OptionsBuilder = new DbContextOptionsBuilder<OrganizationsContext>();
            OptionsBuilder.UseSqlServer(ConnectionString);
            //db = new OrganizationsContext(OptionsBuilder.Options);

        }

        public void Dispose()
        {
            db.Dispose();
        }

        /// <summary>
        /// Універсальний клас для оновлення даних
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Rows"></param>
        public void Update<T>(string Table, params T[] Rows) where T:class
        {
            using (OrganizationsContext dc = new OrganizationsContext(OptionsBuilder.Options))
            {
                Type type = dc.GetType();
                DbSet<T> tb = (DbSet<T>) type.GetProperty(Table).GetValue(dc);
                tb.UpdateRange(Rows);
                dc.SaveChanges();
            }
        }

        /// <summary>
        /// Універсальний клас для вставки даних
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Rows"></param>
        public void Insert<T>(string Table, params T[] Rows) where T : class
        {
            using (OrganizationsContext dc = new OrganizationsContext(OptionsBuilder.Options))
            {
                Type type = dc.GetType();
                DbSet<T> tb = (DbSet<T>)type.GetProperty(Table).GetValue(dc);
                tb.AddRange(Rows);
                dc.SaveChanges();
            }
        }

        public void Delete<T>(string Table, params T[] Rows) where T : class
        {
            using (OrganizationsContext dc = new OrganizationsContext(OptionsBuilder.Options))
            {
                Type type = dc.GetType();
                DbSet<T> tb = (DbSet<T>)type.GetProperty(Table).GetValue(dc);
                tb.RemoveRange(Rows);
                dc.SaveChanges();
            }
        }

        public List<Partner> GetOrganizations(int? UserId)
        {
            using (OrganizationsContext dc = new OrganizationsContext(OptionsBuilder.Options))
            {
                return dc.BD_ORG.FromSqlRaw($"SELECT KOD,KOD_ZKPO,NAZVA_ORG,PodNom,NomSvid,Adresa,N_TEL,Primitka FROM dbo.BD_ORG").ToList();
            }
        }

        public List<Account> GetAccounts(int? PartnerId)
        {
            using (OrganizationsContext dc = new OrganizationsContext(OptionsBuilder.Options))
            {
                return dc.Accounts.FromSqlRaw($"SELECT Id,PartnerId,IBAN,MFO,Status FROM dbo.Accounts WHERE PartnerId={PartnerId}").ToList();
            }
        }

    }
}
//SELECT TOP(1000) [Id]
//  ,[PartnerId]
//  ,[IBAN]
//  ,[MFO]
//  ,[Status]
//FROM[BD_ORG].[dbo].[Accounts]
