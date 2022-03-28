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
    public class CompanyUserRolesRepository : ICompanyUserRolesRepository
    {
        string connectionString = null;
        public CompanyUserRolesRepository(string conn)
        {
            connectionString = conn;
        }
        public void Delete(int UserId, int RoleId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM CompanyUserRoles WHERE (CompanyUserId = @UserId AND RoleId = @RoleId)";
                db.Execute(sqlQuery, new { UserId, RoleId });
            }
        }
        public void Add(string StrRoleId, int UserId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (!(StrRoleId == null || StrRoleId == ""))
                {
                    string[] ArrRoleIdId = StrRoleId.Split('_');
                    var UniqRoleId = ArrRoleIdId.Distinct().ToArray();
                    CompanyUserRoles R = new CompanyUserRoles();
                    R.CompanyUserId = UserId;
                    int t;
                    for (int i = 0; i < UniqRoleId.Length; i++)
                    {
                        if (int.TryParse(UniqRoleId[i], out t))
                        {
                            R.RoleId = Convert.ToInt32(UniqRoleId[i]);
                            db.Execute("INSERT INTO CompanyUserRoles(CompanyUserId, RoleId) VALUES(@CompanyUserId, @RoleId)", R);
                        }
                    }
                }
            }
        }
        public void Update(string StrRoleId, int UserId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (!(StrRoleId == null || StrRoleId == ""))
                {
                    var sqlQuery = "DELETE FROM CompanyUserRoles WHERE CompanyUserId = @UserId";
                    db.Execute(sqlQuery, new { UserId });
                    Add(StrRoleId, UserId);
                }
            }
        }
    }
}
