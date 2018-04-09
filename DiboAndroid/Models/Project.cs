using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace DiboWeb.Models
{
    public class Project : BaseEntity<int>
    {
        [PrimaryKey,AutoIncrement]
        public override int Id { get; set; }

        public string Name { get; set; }

        [Ignore]
        public User Creator { get; set; }
        public int CreatorId { get; set; }
        [Ignore]
        public List<GxProperty> GxProperties { get; set; }
        [Ignore]
        public List<GxPoint> GxPoints { get; set; }
        [Ignore]
        public List<GxLine> GxLines { get; set; }
        [Ignore]
        public List<GeoPoint> GeoPoints { get; set; }
        [Ignore]
        public PropertyTemplate PropertyTemplate { get; set; }
        public int PropertyTemplateId { get; set; }
        [Ignore]
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
