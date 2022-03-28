using System;
using System.Collections.Generic;
using System.Text;
using WebTestApplication5.DAL.Entities;

namespace WebTestApplication5.BLL.DTO
{
    public class CompanyUserRolesDTO
    {
        public int CompanyUserId { get; set; }
        public int RoleId { get; set; }
    }
}
