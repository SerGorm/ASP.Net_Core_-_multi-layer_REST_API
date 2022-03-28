using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.DAL.Entities;

namespace WebTestApplication5.DAL.Interfaces
{
    public interface IUnitOfWork //: IDisposable
    {
        IActionRepository Action { get; }
        IRoleRepository Role { get; }
        ICompanyUserRolesRepository CompanyUserRoles { get; }
        //void Save();
    }
}
