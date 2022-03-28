using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using WebTestApplication5.DAL.Entities;
using WebTestApplication5.DAL.Interfaces;

namespace WebTestApplication5.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        string connectionString = null;
        public RoleRepository(string conn)
        {
            connectionString = conn;
        }
        public List<Role> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Role>("SELECT * FROM Role").ToList();
            }
        }
        public Role Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Role>("SELECT * FROM Role WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public List<Role> GetCompanyRoles(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Role>("SELECT * FROM CompanyRoles JOIN Role ON Role.Id = CompanyRoles.RoleId WHERE CompanyRoles.CompanyId = @id", new { id }).ToList();
            }
        }
        public List<Role> GetUserRoles(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Role>("SELECT * FROM CompanyUserRoles JOIN Role ON Role.Id = CompanyUserRoles.RoleId WHERE CompanyUserRoles.CompanyUserId = @id", new { id }).ToList();
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM CompanyUserRoles WHERE RoleId = @id", new { id });
                db.Execute("DELETE FROM CompanyRoles WHERE RoleId = @id", new { id });
                db.Execute("DELETE FROM RoleActions2 WHERE Role2Id = @id", new { id });
                db.Execute("DELETE FROM Role WHERE Id = @id", new { id });
            }
        }
        public void Create(string StrActId, Role item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (!(item == null || StrActId == ""))
                {
                    var sqlQuery = "INSERT INTO Role(Name, LocalName) VALUES(@Name, @LocalName); SELECT CAST(SCOPE_IDENTITY() as int)";
                    int? curId = db.Query<int>(sqlQuery, item).FirstOrDefault();
                    string[] ActId = StrActId.Split('_');
                    var UniqActId = ActId.Distinct().ToArray();
                    RoleActions2 RA = new RoleActions2();
                    RA.Role2Id = curId.Value;
                    int t;
                    for (int i = 0; i < UniqActId.Length; i++)
                    {
                        if (int.TryParse(UniqActId[i], out t))
                        {
                            RA.ActionId = Convert.ToInt32(UniqActId[i]);
                            db.Execute("INSERT INTO RoleActions2(Role2Id, ActionId) VALUES(@Role2Id, @ActionId)", RA);
                        }
                    }
                }
            }
        }
        public void Update(string StrActId, Role item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (!(item == null || StrActId == ""))
                {
                    var sqlQuery = "UPDATE Role SET Name = @Name, LocalName = @LocalName WHERE Id = @Id";
                    db.Execute(sqlQuery, item);
                    db.Execute("DELETE FROM RoleActions2 WHERE Role2Id = @Id", item);
                    string[] ActId = StrActId.Split('_');
                    var UniqActId = ActId.Distinct().ToArray();
                    RoleActions2 RA = new RoleActions2();
                    RA.Role2Id = item.Id;
                    int t;
                    for (int i = 0; i < UniqActId.Length; i++)
                    {
                        if (int.TryParse(UniqActId[i], out t))
                        {
                            RA.ActionId = Convert.ToInt32(UniqActId[i]);
                            db.Execute("INSERT INTO RoleActions2(Role2Id, ActionId) VALUES(@Role2Id, @ActionId)", RA);
                        }
                    }
                }
            }
        }
    }
}
