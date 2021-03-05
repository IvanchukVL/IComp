//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ICompAccounting.Model.Entities;
using ICompAccounting.Model.Entities.org;
using ICompAccounting.Model.Entities.oper;

namespace ICompAccounting.Model
{
    public class Repository
    {
        public DbContextOptionsBuilder<AccountingContext> OptionsBuilder { set; get; }

        public Repository(string ConnectionString)
        {
            OptionsBuilder = new DbContextOptionsBuilder<AccountingContext>();
            OptionsBuilder.UseSqlServer(ConnectionString);
        }


        /// <summary>
        /// Універсальний клас для оновлення даних
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Rows"></param>
        public void Update<T>(string Table, params T[] Rows) where T:class
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
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
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                Type type = dc.GetType();
                DbSet<T> tb = (DbSet<T>)type.GetProperty(Table).GetValue(dc);
                tb.AddRange(Rows);
                dc.SaveChanges();
            }
        }

        public void Delete<T>(string Table, params T[] Rows) where T : class
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                Type type = dc.GetType();
                DbSet<T> tb = (DbSet<T>)type.GetProperty(Table).GetValue(dc);
                tb.RemoveRange(Rows);
                dc.SaveChanges();
            }
        }


        public List<vmenu> GetMenu(int? UserId)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.vMenu.FromSqlRaw($"SELECT UserId,MenuId,Name,Command,ParentId,Bold FROM dbo.vMenu WHERE UserId={UserId}").ToList();
            }
        }

        public List<Period> GetPeriods()
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.Periods.FromSqlRaw($"SELECT Id,Code,description,ParentId,Status FROM dbo.Periods WHERE Status=1").ToList();
            }
        }

        public List<Enterprise> GetEnterprises()
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.Enterprises.FromSqlRaw($"SELECT Id,Name,Account,MFO,EDRPOU,Year,Period FROM dbo.Enterprises").ToList();
            }
        }

        public List<vReferenceValue> GetReferenceValues(string ReferenceCode)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                string Sql = "SELECT Id,ReferenceCode,Value,Description,Dat1,Dat2,Sort " +
                                            "FROM dbo.vReferenceValues " +
                                           $"WHERE ReferenceCode='{ReferenceCode}' " +
                                            "ORDER BY Sort";
                return dc.vReferenceValues.FromSqlRaw(Sql).ToList();
            }
        }

        public vUser GetUser(string Login)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                List<vUser> list = dc.vUsers.FromSqlRaw($"SELECT Id,Login,PIB,AuthenticationType,Status FROM dbo.vUsers WHERE Login='{Login}'").ToList();
                if (list.Count > 0)
                    return list[0];
                else
                    return null;
            }
        }

        public List<Partner> GetOrganizations(int? UserId)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.Partners.FromSqlRaw($"SELECT KOD,KOD_ZKPO,NAZVA_ORG,PodNom,NomSvid,Adresa,N_TEL,Primitka FROM org.Partners").ToList();
            }
        }

        public List<Account> GetAccounts(int? PartnerId)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.Accounts.FromSqlRaw($"SELECT Id,PartnerId,IBAN,MFO,Status FROM org.Accounts WHERE PartnerId={PartnerId}").ToList();
            }
        }

        public List<AccountPurpose> GetAccountPurposes(int? AccountId)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                var list = dc.AccountPurposes.FromSqlRaw($"SELECT Id,AccountId,OperationId,Purpose,Status FROM org.AccountPurposes WHERE AccountId={AccountId}").ToList();
                //foreach (var row in list)
                //{
                //    row.Operation = GetOperationList().First();
                //}
                return list;

            }
        }

        public List<Operation> GetOperationList()
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.OperationList.FromSqlRaw($"SELECT Id,Code,Description,Status FROM oper.OperationList WHERE Status=1").ToList();
            }
        }


    }
}
