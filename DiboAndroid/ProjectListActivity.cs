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
using DiboWeb.Models;

namespace DiboAndroid
{
    [Activity(Label = "ProjectList")]
    public class ProjectListActivity : Activity
    {
        User _user;
        private IList<IDictionary<string, Object>> GetUserProjects(User user)
        {
            IList<IDictionary<string, Object>> listItems = new List<IDictionary<string, Object>>();
            if(user!=null)
            {
                foreach (var  item in user.UserProjects)
                {
                    Project prj1 = item.Project;
                    IDictionary<string, Object> prjs = new Dictionary<string, Object>();
                    prjs.Add("name", prj1);
                }
            }

            return listItems;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProjectList);
            Button btn = FindViewById<Button>(Resource.Id.button_addproject);
            ListView listView = FindViewById<ListView>(Resource.Id.listView_project);
            listView.ItemClick += ListView_ItemClick;
            btn.Click += Btn_Click;
            // Create your application here
            _user = (User)this.Intent.GetSerializableExtra("user");
          //  SimpleAdapter simpleAdapter = new SimpleAdapter(this,)
         }

       

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}