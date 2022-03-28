using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTestApplication5.DAL.Entities
{
    public class RoleActions2
    {
        public int Role2Id { get; set; }
        [ForeignKey("Role2Id")]
        public Role Role { get; set; }
        public int ActionId { get; set; }
        public Action Action { get; set; }
    }
}
