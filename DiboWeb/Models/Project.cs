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
