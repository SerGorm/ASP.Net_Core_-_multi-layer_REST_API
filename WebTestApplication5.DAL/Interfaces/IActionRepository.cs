using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.DAL.Entities;

namespace WebTestApplication5.DAL.Interfaces
{
    public interface IActionRepository
    {
        List<Entities.Action> GetAll();
        Entities.Action Get(int id);
        List<Entities.Action> GetUserActions(int id);
        string GetCheckUserAction(int UserId, int id);
        void Create(Entities.Action item);
        void Update(Entities.Action item);
        void Delete(int id);
    }
}
