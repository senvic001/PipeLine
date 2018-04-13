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
using DiboAndroid.Services;
using DiboWeb.Models;

namespace DiboAndroid
{
    [Activity(Label = "Sinup")]
    public class SinupActivity : Activity
    {
        EditText phone;
        EditText user;
        EditText pwd;
        //EditText pwd1;
        EditText valid;
        Button bt_register;
        Button bt_verify;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);
            // Create your application here
            phone = FindViewById<EditText>(Resource.Id.rg_phone);
            valid = FindViewById<EditText>(Resource.Id.rg_verify);
            user = FindViewById<EditText>(Resource.Id.rg_user);
            pwd = FindViewById<EditText>(Resource.Id.rg_password);
            bt_register = FindViewById<Button>(Resource.Id.btnRegister);
            bt_verify = FindViewById<Button>(Resource.Id.rg_verify);

            bt_verify.Click += Bt_verify_Click;
            bt_register.Click += Bt_register_Click;
        }

        private void Bt_register_Click(object sender, EventArgs e)
        {
            valid.Text = "123456";
            valid.Invalidate();
        }

        private void Bt_verify_Click(object sender, EventArgs e)
        {
            if(phone.Text.Length == 0 || valid.Text.Length < 7 || user.Text.Length < 3 || pwd.Text.Length <6)
            {
                Toast.MakeText(ApplicationContext, "输入数据格式不正确，请重新输入。", ToastLength.Short).Show();
                return;
            }
            User tmpuser = new User() { Name = user.Text, PassWord = pwd.Text,Phone=phone.Text };
            UserService userService = new UserService(tmpuser, GetString(Resource.String.host));
            var u = userService.Register();
            if (u != null)
            {
                Toast.MakeText(this.ApplicationContext, $"注册成功:欢迎新用户{user.Text}！\n自动登陆中...", ToastLength.Short).Show();
               // Intent intent = new Intent();
            }
            else
            {
                Toast.MakeText(this.ApplicationContext, "注册失败:已存在。", ToastLength.Short).Show();
            }
         }
    }
}