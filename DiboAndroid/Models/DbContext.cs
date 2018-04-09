using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using SQLite;

namespace DiboWeb.Models
{
    //Singleton
    class DbContext :IDisposable
    {
        private static readonly DbContext dbContext=new DbContext();
        private string userData = "userdb";
        private SQLiteConnection db;
        public SQLiteConnection Db { get { return db; }  }
        private DbContext()
        {
            var db = new SQLiteConnection(userData);
            db.CreateTable<User>();
            db.CreateTable<Project>();
            db.CreateTable<UserProject>();
        }
        
        public static DbContext GetDbContext()
        {
            return dbContext;
        }

        void IDisposable.Dispose()
        {
            if (db != null) db.Close();
        }
    }
}