using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiboWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DiboWeb.Service
{
    public class ProjectService:IProjectService
    {
        private  GxDbContext gxDb;

        public ProjectService(GxDbContext dbContext)
        {
            gxDb = dbContext;
        }

        public void AddGeoPoint(Project project, GeoPoint geoPoint)
        {
            project.GeoPoints.Add(geoPoint);
            gxDb.SaveChanges();
        }

        public void AddGeoPoints(Project project, IEnumerable<GeoPoint> geoPoints)
        {
            project.GeoPoints.AddRange(geoPoints);
            gxDb.SaveChanges();
        }

        public void AddGxLine(Project project, GxLine gxLine)
        {
            project.GxLines.Add(gxLine);
            gxDb.SaveChanges();
        }

        public void AddGxLines(Project project, IEnumerable<GxLine> gxLines )
        {
            project.GxLines.AddRange(gxLines);
            gxDb.SaveChanges();
        }

        public void AddGxPoint(Project project, GxPoint gxPoint)
        {
            project.GxPoints.Add(gxPoint);
            gxDb.SaveChanges();
        }

        public void AddUser(Project project, User user)
        {
            project.UserProjects.Add(new UserProject() { User = user, Project = project });
            gxDb.SaveChanges(); 
        }

        public void AddUsers(Project project, IEnumerable<User> users)
        {
            List<UserProject> up = new List<UserProject>(users.Count());
            foreach (var item in users)
            {
                up.Add(new UserProject() { User = item, Project = project });
            }
            project.UserProjects.AddRange(up);
            gxDb.SaveChanges();
        }

        public void DeleteGeoPoint(Project project, GeoPoint geoPoint)
        {
            project.GeoPoints.Remove(geoPoint);
            gxDb.SaveChanges();
        }

        public void DeleteGxLines(Project project, IEnumerable<GxLine> gxLines)
        {
            foreach (var item in gxLines)
            {
                project.GxLines.Remove(item);
            }
            gxDb.SaveChanges();
        }

        public void DeleteGxPoint(Project project, GxPoint gxPoint)
        {
            project.GxPoints.Remove(gxPoint);
            gxDb.SaveChanges();
        }

        public void DeleteUser(Project project, User user)
        {
            UserProject up = project.UserProjects.Single(u => u.UserId == user.Id);
            if(up !=null)
            {
                project.UserProjects.Remove(up);
                gxDb.SaveChanges();
            }
        }

        public void LoadProjectData(ref Project project)
        {
            int id = project.Id;
            int templateid = project.PropertyTemplateId;
            project.UserProjects = gxDb.UserProjects.Where(p => p.ProjectId == id).ToList();
            project.GxPoints = gxDb.GxPoints.Where(p => p.Id == id).ToList();
            project.GxLines = gxDb.GxLines.Where(p => p.Id == id).ToList();
            project.GeoPoints = gxDb.GeoPoints.Where(p => p.Id == id).ToList();

            project.PropertyTemplate.Template_Properties = gxDb.Template_Properties.Where(p => p.PropertyTemplateId ==templateid)
                .Include(u => u.GxProperty)
                .Include(u=>u.PropertyTemplate)
                .ToList();
            foreach (var item in project.PropertyTemplate.Template_Properties)
            {
                project.GxProperties.Add(item.GxProperty);
            }
        }

        public void UpdateGeoPoint(Project project, GeoPoint geoPoint)
        {
            var gp = project.GeoPoints.Single(u => u.Id == geoPoint.Id);
            if(gp != null)
            {
                project.GeoPoints.Remove(gp);
                project.GeoPoints.Add(geoPoint);
                gxDb.SaveChanges();
            }
        }

        public void UpdateGxLine(Project project, GxLine gxLine)
        {
            var gl = project.GxLines.Single(u => u.Id == gxLine.Id);
            if(gl != null)
            {
                project.GxLines.Remove(gl);
                project.GxLines.Add(gxLine);
                gxDb.SaveChanges();
            }
        }

        public void UpdateGxLines(Project project, IEnumerable<GxLine> gxLines)
        {
            foreach (var gl in gxLines)
            {
                var gl2 = project.GxLines.Single(u => u.Id == gl.Id);
                if (gl2 != null)
                {
                    project.GxLines.Remove(gl2);
                    project.GxLines.Add(gl);
                }
            }
            gxDb.SaveChanges();
        }

        public void UpdateGxPoint(Project project, GxPoint gxPoint)
        {
            var gl = project.GxPoints.Single(u => u.Id == gxPoint.Id);
            if (gl != null)
            {
                project.GxPoints.Remove(gl);
                project.GxPoints.Add(gxPoint);
                gxDb.SaveChanges();
            }
        }

        public void UpdateUserProject(Project project ,UserProject userProject)
        {
            var up = project.UserProjects.Single(u => u.ProjectId == userProject.Id);
            if(up!=null)
            {
                project.UserProjects.Remove(up);
                project.UserProjects.Add(userProject);
                gxDb.SaveChanges();
            }
        }
    }
}
