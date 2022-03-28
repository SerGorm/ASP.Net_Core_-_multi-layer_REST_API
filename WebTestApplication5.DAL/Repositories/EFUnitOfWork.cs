using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.DAL.Interfaces;
using WebTestApplication5.DAL.Entities;

namespace WebTestApplication5.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        string connectionString = null;
        private ActionRepository actionRepository;
        private RoleRepository roleRepository;
        private CompanyUserRolesRepository companyUserRolesRepository;
        public EFUnitOfWork(string conn)
        {
            connectionString = conn;
        }
        public IActionRepository Action
        {
            get
            {
                if (actionRepository == null)
                    actionRepository = new ActionRepository(connectionString);
                return actionRepository;
            }
        }
        public IRoleRepository Role
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(connectionString);
                return roleRepository;
            }
        }
        public ICompanyUserRolesRepository CompanyUserRoles
        {
            get
            {
                if (companyUserRolesRepository == null)
                    companyUserRolesRepository = new CompanyUserRolesRepository(connectionString);
                return companyUserRolesRepository;
            }
        }
        /*public void Save()
        {
            connectionString.S
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    connectionString.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }*/
    }
}
