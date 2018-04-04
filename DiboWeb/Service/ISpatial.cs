using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiboWeb.Models;

namespace DiboWeb.Service
{
    interface ISpatial
    {
        //得到相连的点
        IEnumerable<GxPoint> GetLinkedPoints(Project project, Point3D point);
        //返回某个范围内的点
        IEnumerable<GxPoint> GetRangePoints(Project project, Point3D point,double radius);
    }
}
