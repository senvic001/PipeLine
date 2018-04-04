using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiboWeb.Models;

namespace DiboWeb.Service
{
    interface IUserService
    {
        //user
        bool Register(string name, string password);
        User Login(string name, string password);
        void Logout(User user);
        User GetUser(string name);

        //user-project
        bool CreateProject(User user, ref Project project);
        bool DeleteProject(User user, Project project);
        IList<UserProject> GetUserProjects(User user);
        void GetActiveProject(User user, out Project project);
        void SetActiveProject(User user, ref Project project);

        //message
        //bool SendMessage(User from, User to, string message);
    }
}
