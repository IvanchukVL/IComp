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


        public List<BD_ORG> GetOrganizations(int? UserId)
        {
            using (OrganizationsContext dc = new OrganizationsContext(OptionsBuilder.Options))
            {
                return dc.Organizations.FromSqlRaw($"SELECT KOD,KOD_ZKPO,NAZVA_ORG,PodNom FROM dbo.BD_ORG").ToList();
            }
        }
    }
}
