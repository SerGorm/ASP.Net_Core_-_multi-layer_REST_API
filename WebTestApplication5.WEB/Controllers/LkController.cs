using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Mvc;
//using Microsoft.EntityFrameworkCore;
using WebTestApplication5.WEB.Models;
using WebTestApplication5.BLL.DTO;
using WebTestApplication5.BLL.Interfaces;
using WebTestApplication5.BLL.Infrastructure;
using AutoMapper;

namespace WebTestApplication5.WEB.Controllers
{
    [ApiController]
    [Route("api")]
    public class LkController : ControllerBase
    {
        IActionService actionService;
        IRoleService roleService;
        ICompanyUserRolesService companyUserRolesService;
        public LkController(IActionService aserv, IRoleService rserv, ICompanyUserRolesService cserv)
        {
            actionService = aserv;
            roleService = rserv;
            companyUserRolesService = cserv;
        }
        /*public LkController(IRoleService rserv)
        {
            roleService = rserv;
        }
        public LkController(ICompanyUserRolesService cserv)
        {
            companyUserRolesService = cserv;
        }*/

        [HttpGet("Actions")]
        public async Task<ActionResult<IEnumerable<ActionModel>>> GetAction() //Получить все Доступы
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ActionDTO, ActionModel>()).CreateMapper();
            return mapper.Map<List<ActionDTO>, List<ActionModel>>(actionService.GetAll());
        }

        [HttpGet("Action/{id}")]
        public async Task<ActionResult<ActionModel>> GetAction(int id) /////Получить Доступ по Id
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ActionDTO, ActionModel>()).CreateMapper();
            return mapper.Map<ActionDTO, ActionModel>(actionService.Get(id));
        }
        
        [HttpGet("Roles")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetAllRoles() //Получить все Роли
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleModel>()).CreateMapper();
            return mapper.Map<List<RoleDTO>, List<RoleModel>>(roleService.GetAll());
        }

        [HttpGet("Role/{id}")]
        public async Task<ActionResult<RoleModel>> GetOneRole(int id) /////Получить Роль по Id
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleModel>()).CreateMapper();
            return mapper.Map<RoleDTO, RoleModel>(roleService.Get(id));
        }

        [HttpGet("Roles/Company/{id}")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetCompanyRoles(int id) //Получить все Роли по Id УК
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleModel>()).CreateMapper();
            return mapper.Map<List<RoleDTO>, List<RoleModel>>(roleService.GetCompanyRoles(id));
        }

        [HttpGet("Roles/User/{id}")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetCompanyUserRoles(int id) //Получить Роли Сотрудника по его Id
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleModel>()).CreateMapper();
            return mapper.Map<List<RoleDTO>, List<RoleModel>>(roleService.GetUserRoles(id));
        }
        
        [HttpGet("Actions/User/{id}")]
        public async Task<ActionResult<IEnumerable<ActionModel>>> GetCompanyUserActions(int id) //Получить Доступы Сотрудника по его Id
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ActionDTO, ActionModel>()).CreateMapper();
            return mapper.Map<List<ActionDTO>, List<ActionModel>>(actionService.GetUserActions(id));
        }

        [HttpGet("Check/User/{UserId}/Action/{id}")]
        public async Task<ActionResult> GetCheckUserAction(int UserId, int id) //Проверить наличие у Сотрудника Доступа по Id Сотрудника и Id Доступа
        {
            return Ok(actionService.GetCheckUserAction(UserId, id));
        }
        
        [HttpDelete("Delete/User/{id}/Role/{RoleId}")]
        public async Task<ActionResult> DeleteUserRole(int id, int RoleId) //Удалить Роль у Сотрудника
        {
            companyUserRolesService.Delete(id, RoleId);
            return Ok("Роль удалена");
        }

        [HttpPost("Add/Roles/{StrRoleId}/User/{id}")]
        public async Task<ActionResult> AddUserRole(string StrRoleId, int id) //Добавить Роли Сотруднику
        {
            //if (StrRoleId == null || StrRoleId == "") return BadRequest();
            companyUserRolesService.Add(StrRoleId, id);
            return Ok("Пользователю добавлены новые роли");
        }

        [HttpPost("Update/Roles/{StrRoleId}/User/{id}")]
        public async Task<ActionResult> UpdateUserRoles(string StrRoleId, int id) //Установить набор Ролей для Сотрудника
        {
            //if (StrRoleId == null || StrRoleId == "") return BadRequest();
            companyUserRolesService.Update(StrRoleId, id);
            return Ok("Роли пользователя обновлены");
        }

        [HttpDelete("Delete/Role/{Id}")]
        public async Task<ActionResult> DeleteRole(int id) //Удалить Роль
        {
            roleService.Delete(id);
            return Ok("Роль удалена");
        }

        [HttpPost("Update/Role/{StrActId}")]
        public async Task<ActionResult> UpAct(string StrActId, RoleModel CurRole) //Добавить или изменить Роль
        {
            //if (CurRole == null || StrActId == "") return BadRequest();
            RoleDTO R = new RoleDTO
            {
                Id = CurRole.Id,
                Name = CurRole.Name,
                LocalName = CurRole.LocalName
            };
            /*if (roleService.Get(CurRole.Id).Id != CurRole.Id)
            {
                R.Id = 0;
                roleService.Create(StrActId, R);
            } else
            {
                roleService.Update(StrActId, R);
            }*/
            try
            {
                roleService.Get(CurRole.Id);
                roleService.Update(StrActId, R);
            }
            catch (ValidationException ex)
            {
                roleService.Create(StrActId, R);
            }
            return Ok("Таблица ролей обновлена");
        }
    }
}
