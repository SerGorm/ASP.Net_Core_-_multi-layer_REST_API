using System;
using System.Collections.Generic;
using System.Text;

namespace WebTestApplication5.DAL.Entities
{
    public class CompanyUserRoles
    {
        public int CompanyUserId { get; set; }
        public CompanyUsers CompanyUser { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
