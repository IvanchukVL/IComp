//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AccountingContext.dl
{
    public class Repository
    {
        public DbContextOptionsBuilder<AccountingContext> OptionsBuilder { set; get; }

        public Repository(string ConnectionString)
        {
            OptionsBuilder = new DbContextOptionsBuilder<AccountingContext>();
            OptionsBuilder.UseSqlServer(ConnectionString);
        }

        public async Task<List<vmenu>> GetMenu(int? UserId)
        {
            using (AccountingContext dc = new AccountingContext(OptionsBuilder.Options))
            {
                return await dc.vMenu.FromSqlRaw($"SELECT UserId,MenuId,Name,Command,ParentId FROM dbo.vMenu WHERE UserId={UserId}").ToListAsync();
            }
        }



    }
}
