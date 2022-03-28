using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.BLL.DTO;

namespace WebTestApplication5.BLL.Interfaces
{
    public interface IRoleService
    {
        List<RoleDTO> GetAll();
        RoleDTO Get(int? id);
        List<RoleDTO> GetCompanyRoles(int? id);
        List<RoleDTO> GetUserRoles(int? id);
        void Create(string StrActId, RoleDTO item);
        void Update(string StrActId, RoleDTO item);
        void Delete(int id);
    }
}
