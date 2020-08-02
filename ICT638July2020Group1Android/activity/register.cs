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
    [Activity(Label = "register")]
    public class register : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.register_a);
            base.OnCreate(savedInstanceState);

            EditText editfirstname = FindViewById<EditText>(Resource.Id.custom_firstname);
            EditText editlastname= FindViewById<EditText>(Resource.Id.custom_lastname);
            EditText editaddress = FindViewById<EditText>(Resource.Id.custom_address);
            EditText editusername = FindViewById<EditText>(Resource.Id.custom_username);
            EditText editphonenumber = FindViewById<EditText>(Resource.Id.custom_phonenumber);
            EditText editpassword = FindViewById<EditText>(Resource.Id.custom_password);
            EditText editcountry = FindViewById<EditText>(Resource.Id.custom_country);
            Button btnregister = FindViewById<Button>(Resource.Id.cou_register);
            Button btnback = FindViewById<Button>(Resource.Id.register_back);
            btnregister.Click += Btnregister_Clck;
            btnback.Click += Btnback_Click;
        }

        private void Btnregister_Clck(object sender, EventArgs e)
        {
            //not finsh
            throw new NotImplementedException();
        }

        private void Btnback_Click(object sender, EventArgs e)
        {
            Intent startnavigation = new Intent(this, typeof(Navigation));
            StartActivity(startnavigation);
        }
    }
}