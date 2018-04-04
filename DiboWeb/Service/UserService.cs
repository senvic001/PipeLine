using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiboWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DiboWeb.Service
{
    public class UserService : IUserService
    {
        private  GxDbContext gxDb;

        public UserService(GxDbContext dbContext)
        {
             gxDb= dbContext;
        }
        public bool CreateProject(User user, ref Project project)
        {
            if (user == null || project == null) throw new ArgumentNullException("Create project:null parameter.");
            using (var transaction = gxDb.Database.BeginTransaction())
            {
                try
                {
                    //project name must be unique
                    string name = project.Name;
                    var prjs = gxDb.Projects.Where(u => u.Name == name && u.CreatorId == user.Id).ToList();
                    if (prjs.Count != 0) return false;

                    project.Creator = user;
                    //property template:default: ID=1
                    project.PropertyTemplate = gxDb.PropertyTemplates.Single(u => u.Id == 1);

                    gxDb.Projects.Add(project);
                    gxDb.SaveChanges();
                    //creator
                    UserProject userProject = new UserProject
                    {
                        User = user,
                        Project = project
                    };
                    gxDb.UserProjects.Add(userProject);
                    gxDb.SaveChanges();
                  
                    transaction.Commit();

                    //created project will be active defaultly.
                    SetActiveProject(user, ref project);
                    return true;
                }

                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool DeleteProject(User user, Project project)
        {
            if (user == null || project == null) throw new ArgumentNullException("Create project:null parameter.");
            using (var transaction = gxDb.Database.BeginTransaction())
            {
                try
                {
                    if (project.CreatorId != user.Id) return false;
                    //级联删除 点 线 和userProject记录
                    gxDb.Projects.Remove(project);
                    gxDb.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void GetActiveProject(User user, out Project project)
        {
            project = null;
            foreach (var up in user.UserProjects)
            {
                if (up.Active) project = up.Project;
            }          
        }

        public User GetUser(string name)
        {
            var users = gxDb.Users.Where(u => u.Name == name).ToList();
            return (users.Count == 1) ? users[0] : null;
        }

        public IList<UserProject> GetUserProjects(User user)
        {
            return   gxDb.UserProjects
                .Include(up => up.Project)
                .Where(up => up.UserId == user.Id) 
                .ToList();
        }

        public User Login(string name, string password)
        {
            var users = gxDb.Users
                .Include(u => u.UserProjects)
                    .ThenInclude(up => up.Project)
                .Where(u => u.Name == name && u.PassWord == password).ToList();
            return (users.Count == 1)? users[0] : null;
        }

        public void Logout(User user)
        {
            throw new NotImplementedException();
        }
        public bool Register(ref User user)
        {
            string name = user.Name;

            using (var tran = gxDb.Database.BeginTransaction())
            {
                var users = gxDb.Users.Where(u => u.Name == name).ToList();
                if (users.Count > 0) return false;
                gxDb.Users.Add(user);
                gxDb.SaveChanges();
                tran.Commit();
                return true;
            }
        }
        public bool Register(string name, string password)
        {
            using (var tran = gxDb.Database.BeginTransaction())
            {
                try
                {
                    var user = gxDb.Users.Where(u => u.Name == name).ToList();
                    if (user.Count >= 1) return false;

                    gxDb.Users.Add(new User() { Name = name, PassWord = password });
                    gxDb.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void SetActiveProject(User user, ref Project project)
        {
            using (var tran = gxDb.Database.BeginTransaction())
            {
                foreach (var pr in user.UserProjects)
                {
                    if (pr.Active)
                    {
                        pr.Active = false;
                        gxDb.UserProjects.Update(pr);
                    }
                    if (pr.ProjectId == project.Id)
                    {
                        pr.Active = true;
                        gxDb.UserProjects.Update(pr);
                    }
                }
                gxDb.SaveChanges();
                tran.Commit();
            }
        }
    }
}
