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
    public class Repository:IDisposable
    {
        public DbContextOptionsBuilder<AccountingContext> OptionsBuilder { set; get; }
        public AccountingContext dc;

        public Repository(string ConnectionString)
        {
            OptionsBuilder = new DbContextOptionsBuilder<AccountingContext>();
            OptionsBuilder.UseSqlServer(ConnectionString);
        }

        public void Open()
        {
            dc = new AccountingContext(OptionsBuilder.Options);
        }

        public void Close()
        {
            dc.Dispose();
        }
        public void Dispose()
        {
            if (dc!=null)
                dc.Dispose();
        }

        ~Repository()
        {
            if (dc != null)
                dc.Dispose();
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
                return dc.Enterprises.FromSqlRaw($"SELECT Id,Name,Account,MFO,EDRPOU FROM dbo.Enterprises").ToList();
            }
        }

        public UsersLocalParam GetUsersLocalParams(int? UserId, int? EnterpriseId)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                var res = dc.UsersLocalParams.FromSqlRaw($"SELECT UserId,EnterpriseId,Year,Period FROM dbo.UsersLocalParams WHERE UserId={UserId} AND EnterpriseId={EnterpriseId}").ToList();
                return res?.Count()>0?res.First():null;
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

        public List<Partner> GetPartners(int? UserId)
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

        public DbSet<AccountPurpose> GetAccountPurposes(int? AccountId)
        {
            dc.AccountPurposes.Where(x=>x.AccountId==AccountId).Load();
            return dc.AccountPurposes;

            //var list = dc.AccountPurposes.FromSqlRaw($"SELECT Id,AccountId,OperationId,Purpose,Status FROM org.AccountPurposes WHERE AccountId={AccountId}").Load();
        }

        public List<Operation> GetOperationList()
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.OperationList.FromSqlRaw($"SELECT Id,Code,Description,Status FROM oper.OperationList WHERE Status=1").ToList();
            }
        }

        public List<vOperationOut> OperationsOut(int BankId, DateTime? Dat)
        {
            int? DatInt = Dat.ToInt();
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                var list = dc.OperationsOut
                    .GroupJoin(dc.Partners, o=>o.PartnerId,p=>p.KOD,(o,p)=> new { o, p } )
                    .SelectMany(xy => xy.p.DefaultIfEmpty(), (x, y) => new vOperationOut {  OperationOut = x.o,  Partner = y })
                    .ToList();
                return list;
            }
        }

        public Partner GetPartner(int Id)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.Partners.Where(x => x.KOD == Id)?.Single();
            }
        }

    }
}
