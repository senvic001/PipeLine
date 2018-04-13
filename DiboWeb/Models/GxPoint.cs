using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class GxPoint :BaseEntity<long>
    {
        public override long Id { get; set; }

        public string MapNo { get; set; }

        public string ExpNo { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        //
        public  GeometryStatus PointStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }

        //照片
        List<Photo> Photos { get; set; }

       // public List<GxLine> LinkedLines { get; set; }

        //属性在数据库中存为string,对象中转为Dictionary
        public Dictionary<GxProperty, string> GxProperties { get; set; }
        public string PropertiyString { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
