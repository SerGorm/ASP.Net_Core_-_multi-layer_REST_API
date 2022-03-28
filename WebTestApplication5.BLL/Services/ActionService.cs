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
    public class ActionService : IActionService
    {
        IUnitOfWork Database { get; set; }
        public ActionService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public List<ActionDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Entities.Action, ActionDTO>()).CreateMapper();
            return mapper.Map<List<DAL.Entities.Action>, List<ActionDTO>>(Database.Action.GetAll());
        }
        public ActionDTO Get(int? id)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Entities.Action, ActionDTO>()).CreateMapper();
            //return mapper.Map<DAL.Entities.Action, ActionDTO>(Database.Action.Get(id));
            if (id == null)
                throw new ValidationException("Не установлен id доступа","");
            var act = Database.Action.Get(id.Value);
            if (act == null)
                throw new ValidationException("Доступ не найден","");
            return new ActionDTO { Id = act.Id, Name = act.Name, LocalName = act.LocalName };
        }
        public List<ActionDTO> GetUserActions(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен id сотрудника", "");
            /*var act = Database.Action.Get(id.Value);
            if (act == null)
                throw new ValidationException("Сотрудник не найден", "");*/
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Entities.Action, ActionDTO>()).CreateMapper();
            return mapper.Map<List<DAL.Entities.Action>, List<ActionDTO>>(Database.Action.GetUserActions(id.Value));
        }
        public string GetCheckUserAction(int UserId, int id)
        {
            return Database.Action.GetCheckUserAction(UserId, id);
        }
        public void Create(ActionDTO item)
        {
            DAL.Entities.Action act = new DAL.Entities.Action
            {
                Name = item.Name,
                LocalName = item.LocalName
            };
            Database.Action.Create(act);
        }
        public void Update(ActionDTO item)
        {
            DAL.Entities.Action act = new DAL.Entities.Action
            {
                Id = item.Id,
                Name = item.Name,
                LocalName = item.LocalName
            };
            Database.Action.Update(act);
        }
        public void Delete(int id)
        {
            Database.Action.Delete(id);
        }
    }
}
