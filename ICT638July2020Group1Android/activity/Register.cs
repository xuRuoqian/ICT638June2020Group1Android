 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace ICT638July2020Group1Android.activity
{
    [Activity(Label = "Regsiter")]
    public class Register : Activity
    {
        private EditText txt_fname, txt_lname, txt_address, txt_country, txt_phoneNum, txt_username, txt_password;
        private Button btn_register;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.register);
            btn_register = FindViewById<Button>(Resource.Id.btn_register);

            txt_fname = FindViewById<EditText>(Resource.Id.txt_reg_fname);
            txt_lname = FindViewById<EditText>(Resource.Id.txt_reg_lname);
            txt_phoneNum = FindViewById<EditText>(Resource.Id.txt_reg_phonenum);
            txt_address = FindViewById<EditText>(Resource.Id.txt_reg_address);
            txt_country = FindViewById<EditText>(Resource.Id.txt_reg_country);
            txt_username = FindViewById<EditText>(Resource.Id.txt_reg_username);
            txt_password = FindViewById<EditText>(Resource.Id.txt_reg_pwd);

            btn_register.Click += (sender, e) =>
            {
                Models.User user = new Models.User();
                user.Fname = txt_fname.Text;
                user.Lname = txt_lname.Text;
                user.PhoneNum = txt_phoneNum.Text;
                user.Address = txt_address.Text;
                user.Country = txt_country.Text;
                user.UserName = txt_username.Text;
                user.Password = txt_password.Text;
                //@insertlink
                var request = HttpWebRequest.Create(string.Format(@"https://localhost:5001/api/users"));
                request.ContentType = "application/json";
                request.Method = "POST";

                var userJason = JsonConvert.SerializeObject(user);

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {

                    streamWriter.Write(userJason);
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                        Toast.MakeText(this, "Failed to create user. Please retry!", ToastLength.Long);
                    }
                    else
                    {
                        Toast.MakeText(this, "Registered successfully", ToastLength.Long);


                        Intent LoginIntent = new Intent(this, typeof(Login));
                        StartActivity(LoginIntent);
                    }
                }
            };

        }


    }
}