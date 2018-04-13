using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiboWeb.Models
{
    public class GeoPoint:BaseEntity<long>
    {
        public override long Id { get; set; }

        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public GeometryStatus PointStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }
    }
}
