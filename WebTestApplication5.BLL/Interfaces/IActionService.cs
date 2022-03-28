using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.BLL.DTO;

namespace WebTestApplication5.BLL.Interfaces
{
    public interface IActionService
    {
        List<ActionDTO> GetAll();
        ActionDTO Get(int? id);
        List<ActionDTO> GetUserActions(int? id);
        string GetCheckUserAction(int UserId, int id);
        void Create(ActionDTO item);
        void Update(ActionDTO item);
        void Delete(int id);
    }
}
