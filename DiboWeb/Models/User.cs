using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class User : BaseEntity<int>
    {
        public override int Id { get; set; }

        public string Name { get; set; }
        public string PassWord { get; set; }
        public string Phone { get; set; }

        public int Status { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RegisterTime { get; set; }
        public string RegisterIp { get; set; }
        public DateTime LoginTime { get; set; }
        public string LoginIp { get; set; }
        public string Email { get; set; }
        //
        public string Company { get; set; }

        //头像
        public int AvatarId { get; set; }
        public Avatar Avatar { get; set; }



        //用户创建的项目
        public List<Project> CreatedProjects { get; set; }
        //用户参与的项目(包含创建的项目)
        public List<UserProject> UserProjects { get; set; }

        public User()
        {
            UserProjects = new List<UserProject>();
            CreatedProjects = new List<Project>();
        }
    }
}
