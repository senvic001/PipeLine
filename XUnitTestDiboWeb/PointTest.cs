using DiboWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace XUnitTestDiboWeb
{
    public class PointTest
    {
        private readonly ITestOutputHelper output;

        public PointTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Point3DTest()
        {
            Point3D pt1 = new Point3D(100.2, 100.3, 2.1);
            Point3D pt2 = new Point3D(100.3, 100.4, 2.1);
            Point3D pt3 = new Point3D(110.2, 110.3, 2.1);
            Point3D pt4 = new Point3D(120.2, 130.3, 2.1);
            Point3D pt5 = new Point3D(100.2, 100.3, 2.1);
           var pts = new Point3D[] { pt1, pt2, pt3, pt4, pt5 };
            foreach (var pt in pts)
            {
                //System.Diagnostics.Debug.WriteLine("Point:{0}\t Hash code:{1}",pt,pt.GetHashCode());
                // Console.WriteLine("Point:{0}\t Hash code:{1}", pt, pt.GetHashCode());
                // System.Diagnostics.Trace.WriteLine($"Point:{pt.ToString()}\t Hash code:{pt.GetHashCode()}");
                output.WriteLine($"Point:{pt.ToString()}\t Hash code:{pt.GetHashCode()}");
            }
            Assert.Equal(pt1, pt5);
            Assert.NotEqual(pt1, pt2);
            Assert.True(Point3D.Equals2D(pt1, 100.2, 100.3));
        }
    }
}
