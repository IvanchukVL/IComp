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
    public class Repository
    {
        public DbContextOptionsBuilder<AccountingContext> OptionsBuilder { set; get; }

        public Repository(string ConnectionString)
        {
            OptionsBuilder = new DbContextOptionsBuilder<AccountingContext>();
            OptionsBuilder.UseSqlServer(ConnectionString);
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

        public List<vEnterprise> GetEnterprises()
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return dc.vEnterprises.FromSqlRaw($"SELECT Id,Name,Account,MFO,EDRPOU FROM dbo.Enterprise").ToList();
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


    }
}
