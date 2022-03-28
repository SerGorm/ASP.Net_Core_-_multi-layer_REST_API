using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.DAL.Entities;

namespace WebTestApplication5.DAL.Interfaces
{
    public interface ICompanyUserRolesRepository
    {
        void Delete(int UserId, int RoleId);
        void Add(string StrRoleId, int UserId);
        void Update(string StrRoleId, int UserId);
    }
}
