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

namespace ICT638July2020Group1Android.activity
{
    public class homelogin : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.homelogin);
            Button btncustomerlogin = FindViewById<Button>(Resource.Id.custom_login);
            Button btnagentlogin = FindViewById<Button>(Resource.Id.agent_login);
            btncustomerlogin.Click += Btncustomerlogin_Click;
            btnagentlogin.Click += Btnagentlogin_Click;
        }

        private void Btnagentlogin_Click(object sender, EventArgs e)
        {
            Intent RegisterIntent = new Intent(this, typeof(SignUp));
            StartActivity(RegisterIntent);
        }

        private void Btncustomerlogin_Click(object sender, EventArgs e)
        {
            Intent RegisterIntent = new Intent(this, typeof(SignUp));
            StartActivity(RegisterIntent);
        }
    }
}