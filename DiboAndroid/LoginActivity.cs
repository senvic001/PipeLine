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
using DiboAndroid.Services;

namespace DiboAndroid
{
    [Activity(Label = "@string/login", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        EditText phone ;
        EditText password ;
        Button btn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.lgoin);
            phone =FindViewById<EditText>(Resource.Id.login_phone);
            password = FindViewById<EditText>(Resource.Id.login_password);
            btn = FindViewById<Button>(Resource.Id.btnLogin);
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if(phone.Text.Length == 0)
            {
                Toast.MakeText(this.ApplicationContext, "用户名不能为空.", ToastLength.Short).Show();
                return;
            }
            if (password.Text.Length == 0)
            {
                Toast.MakeText(this.ApplicationContext, "密码不能为空.", ToastLength.Short).Show();
                return;
            }
            User user = new User() { Name = phone.Text, PassWord = password.Text };
            UserService userService = new UserService(user, GetString(Resource.String.host));
            var u = userService.Login();
            if (u !=null)
            {
                Toast.MakeText(this.ApplicationContext, u.Name, ToastLength.Short).Show();
            } 
            else
            {
                Toast.MakeText(this.ApplicationContext, "登录失败:用户名或密码错误!", ToastLength.Short).Show();
            }
        }
    }
}