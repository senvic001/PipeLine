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

namespace DiboAndroid
{
    [Activity(Label = "注册")]
    public class RegisterActivity : Activity
    {
        private EditText rg_phone;
        private EditText rg_verify;
        private Button bt_verify;
        private EditText rg_password;
        private Button bt_showPassword;
        private Button bt_register;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            rg_phone = FindViewById<EditText>(Resource.Id.rg_phone);
            rg_verify = FindViewById<EditText>(Resource.Id.rg_verify);
            bt_verify = FindViewById<Button>(Resource.Id.bt_verify);
            rg_password = FindViewById<EditText>(Resource.Id.rg_password);
            bt_register = FindViewById<Button>(Resource.Id.btnRegister);

        }
    }
}