using System;
using WebTestApplication5.BLL.DTO;
using WebTestApplication5.DAL.Entities;
using WebTestApplication5.DAL.Interfaces;
using WebTestApplication5.BLL.Infrastructure;
using WebTestApplication5.BLL.Interfaces;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace WebTestApplication5.BLL.Services
{
    public class CompanyUserRolesService : ICompanyUserRolesService
    {
        IUnitOfWork Database { get; set; }
        public CompanyUserRolesService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Delete(int UserId, int RoleId)
        {
            Database.CompanyUserRoles.Delete(UserId, RoleId);
        }
        public void Add(string StrRoleId, int UserId)
        {
            Database.CompanyUserRoles.Add(StrRoleId, UserId);
        }
        public void Update(string StrRoleId, int UserId)
        {
            Database.CompanyUserRoles.Update(StrRoleId, UserId);
        }
    }
}
