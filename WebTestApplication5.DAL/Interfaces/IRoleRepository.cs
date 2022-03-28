using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.DAL.Entities;

namespace WebTestApplication5.DAL.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> GetAll();
        Role Get(int id);
        List<Role> GetCompanyRoles(int id);
        List<Role> GetUserRoles(int id);
        void Create(string StrActId, Role item);
        void Update(string StrActId, Role item);
        void Delete(int id);
    }
}
