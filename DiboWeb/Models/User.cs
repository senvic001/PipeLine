using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class User : BaseEntity<int>
    {
        public override int Id { get; set; }

        public string Name { get; set; }
        public string PassWord { get; set; }

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
