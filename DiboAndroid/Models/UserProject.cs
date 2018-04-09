using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace DiboWeb.Models
{
    public class UserProject : BaseEntity<int>
    {
        //联合主键,不需要自身ID
       // public override int Id { get; set; }
       [PrimaryKey,AutoIncrement]
        public int UserId { get; set; }
        [Ignore]
        public User User { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

        public int ProjectId { get; set; }
        [Ignore]
        public Project Project { get; set; }
    }
}
