using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class Project : BaseEntity<int>
    {
        public override int Id { get; set; }

        public string Name { get; set; }

        public User Creator { get; set; }
        public int CreatorId { get; set; }

        //项目属性
        public string Company { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime UpdateTime {get;set;}
        public int PointNumbers { get; set; }
        public double LineLength { get; set; }

        //头像
        public int AvatarId { get; set; }
        public Avatar Avatar { get; set; }

        public List<GxProperty> GxProperties { get; set; }

        public List<GxPoint> GxPoints { get; set; }

        public List<GxLine> GxLines { get; set; }

        public List<GeoPoint> GeoPoints { get; set; }

        public PropertyTemplate PropertyTemplate { get; set; }
        public int PropertyTemplateId { get; set; }

        public List<UserProject> UserProjects { get; set; }

        public Project()
        {
            GxPoints = new List<GxPoint>();
            GxLines = new List<GxLine>();
            GxProperties = new List<GxProperty>();
            GeoPoints = new List<GeoPoint>();
            UserProjects = new List<UserProject>();
        }
    }
}
