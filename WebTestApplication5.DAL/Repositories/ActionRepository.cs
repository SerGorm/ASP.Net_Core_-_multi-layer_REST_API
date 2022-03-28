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
    public class ActionRepository : IActionRepository
    {
        string connectionString = null;
        public ActionRepository(string conn)
        {
            connectionString = conn;
        }
        public List<Entities.Action> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Entities.Action>("SELECT * FROM Action").ToList();
            }
        }
        public Entities.Action Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Entities.Action>("SELECT * FROM Action WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public List<Entities.Action> GetUserActions(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Entities.Action>("SELECT DISTINCT * FROM (SELECT ActionId FROM (SELECT RoleId FROM CompanyUserRoles WHERE CompanyUserId = @id) q JOIN RoleActions2 ON RoleActions2.Role2Id = q.RoleId) b JOIN Action ON Action.Id = b.ActionId", new { id }).ToList();
            }
        }
        public string GetCheckUserAction(int UserId, int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var CurUserActions = db.Query<Entities.Action>("SELECT * FROM (SELECT ActionId FROM (SELECT RoleId FROM CompanyUserRoles WHERE CompanyUserId = @UserId) q JOIN RoleActions2 ON RoleActions2.Role2Id = q.RoleId) b JOIN Action ON Action.Id = b.ActionId WHERE Action.Id = @id", new { UserId, id }).ToList();
                if (CurUserActions == null || CurUserActions.Count == 0)
                    return "Доступа нет";
                return "Доступ есть";
            }
        }
        public void Create(Entities.Action item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Action(Name, LocalName) VALUES(@Name, @LocalName)";
                db.Execute(sqlQuery, item);
            }
        }
        public void Update(Entities.Action item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Action SET Name = @Name, LocalName = @LocalName WHERE Id = @Id";
                db.Execute(sqlQuery, item);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Action WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
