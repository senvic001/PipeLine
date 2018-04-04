using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiboWeb.Models;

namespace DiboWeb.Service
{
    interface IProjectService
    {
        //user-user
        void AddUser(Project project,User user);
        void AddUsers(Project project, IEnumerable<User> users);
        void DeleteUser(Project project,User user);
        void UpdateUserProject(Project project,UserProject userProject);

        //data
        void AddGxPoint(Project project, GxPoint gxPoint);
        void DeleteGxPoint(Project project, GxPoint gxPoint);
        void UpdateGxPoint(Project project, GxPoint gxPoint);

        void AddGxLine(Project project, GxLine gxLine);
        void AddGxLines(Project project, IEnumerable<GxLine> gxLines);
        void DeleteGxLines(Project project, IEnumerable<GxLine> gxLines);
        void UpdateGxLine(Project project, GxLine gxLine);
        void UpdateGxLines(Project project, IEnumerable<GxLine> gxLines);

        void AddGeoPoint(Project project, GeoPoint geoPoint);
        void AddGeoPoints(Project project, IEnumerable<GeoPoint> geoPoints);
        void DeleteGeoPoint(Project project, GeoPoint geoPoint);
        void UpdateGeoPoint(Project project, GeoPoint geoPoint);
        void LoadProjectData(ref Project project);
    }
}
