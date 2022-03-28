using System;
using System.Collections.Generic;
using System.Text;

namespace WebTestApplication5.DAL.Entities
{
    public class CompanyRoles
    {
        public int CompanyId { get; set; }
        public Companies Company { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
