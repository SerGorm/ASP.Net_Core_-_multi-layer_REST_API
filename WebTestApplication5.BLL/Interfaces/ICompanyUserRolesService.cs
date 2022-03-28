using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.BLL.DTO;

namespace WebTestApplication5.BLL.Interfaces
{
    public interface ICompanyUserRolesService
    {
        void Delete(int UserId, int RoleId);
        void Add(string StrRoleId, int UserId);
        void Update(string StrRoleId, int UserId);
    }
}
