using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTestApplication5.DAL.Entities
{
    public class CompanyUsers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Companies Companies { get; set; }
        /*
        public int RoleId { get; set; }
        public Role Role { get; set; }
        */
    }
}
