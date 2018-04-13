using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class GxLine:BaseEntity<long>
    {
        public override long Id { get; set; }

        public GxPoint StartPoint { get; set; }
        public long StartPointId { get; set; }

        public GxPoint EndPoint { get; set; }
        public long EndPointId { get; set; }

        public double StartDeep { get; set; }

        public double EndDeep { get; set; }

        //
        public GeometryStatus PointStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }

        //属性在数据库中存为string,对象中转为Dictionary
        public Dictionary<GxProperty, string> GxProperties { get; set; }
        public string PropertiyString { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
