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
    public class RoleService : IRoleService
    {
        IUnitOfWork Database { get; set; }
        public RoleService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public List<RoleDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<List<Role>, List<RoleDTO>>(Database.Role.GetAll());
        }
        public RoleDTO Get(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен id роли", "");
            var r = Database.Role.Get(id.Value);
            if (r == null)
                throw new ValidationException("Роль не найдена", "");
            return new RoleDTO { Id = r.Id, Name = r.Name, LocalName = r.LocalName };
        }
        public List<RoleDTO> GetCompanyRoles(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен id компании", "");
            /*var r = Database.Role.GetCompanyRoles(id.Value);
            if (r == null)
                throw new ValidationException("Роли не найдены", "");*/
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<List<Role>, List<RoleDTO>>(Database.Role.GetCompanyRoles(id.Value));
        }
        public List<RoleDTO> GetUserRoles(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен id сотрудника", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<List<Role>, List<RoleDTO>>(Database.Role.GetUserRoles(id.Value));
        }
        public void Delete(int id)
        {
            Database.Role.Delete(id);
        }
        public void Create(string StrActId, RoleDTO item)
        {
            if (StrActId == null || StrActId == "")
                throw new ValidationException("Не установлены id доступов", "");
            Role r = new Role
            {
                Name = item.Name,
                LocalName = item.LocalName
            };
            Database.Role.Create(StrActId, r);
        }
        public void Update(string StrActId, RoleDTO item)
        {
            if (StrActId == null || StrActId == "")
                throw new ValidationException("Не установлены id доступов", "");
            Role r = new Role
            {
                Id = item.Id,
                Name = item.Name,
                LocalName = item.LocalName
            };
            Database.Role.Update(StrActId, r);
        }
    }
}
