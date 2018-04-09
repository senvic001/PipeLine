using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using DiboWeb.Models;
using RestSharp;

namespace DiboAndroid.Services
{
    class UserService
    {
        private readonly User _user;
        private string baseUrl;

        public UserService(User user,string url)
        {
            baseUrl = url;
            _user = user;
        }
        public User Login()
        {
            User user = new User() { Name = _user.Name, PassWord = _user.PassWord };
            var client = new RestClient(baseUrl);
            var quest = new RestRequest(Method.POST);
            quest.Resource = "user/login";
            quest.AddJsonBody(user);

            var result = client.Execute<User>(quest);
            if( result.ResponseStatus == ResponseStatus.Completed)
            {
               
                return result.Data as User;
            } 
            return null;
        }

        public bool LoginOut(User user)
        {
            return false;
        }
        public bool Register(User user)
        {
            return false;
        }

        public bool CreateProject(ref Project project)
        {
            return false;
        }
        public bool DeleteProject(Project project)
        {
            return false;
        }

        public bool UpdateProject(Project project)
        {
            return false;
        }
    }
}