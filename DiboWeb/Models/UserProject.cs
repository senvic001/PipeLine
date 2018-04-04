using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class UserProject : BaseEntity<int>
    {
        //联合主键,不需要自身ID
       // public override int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
