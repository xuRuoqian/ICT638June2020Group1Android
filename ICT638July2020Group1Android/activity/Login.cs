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

namespace ICT638July2020Group1Android.activity
{
    [Activity(Label = "Login", MainLauncher = true)]

    public class Login : Activity
    {
        private EditText txt_username, txt_pwd;
        private Button btn_login;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login);

            txt_username = FindViewById<EditText>(Resource.Id.editusernamelogin);
            txt_pwd = FindViewById<EditText>(Resource.Id.editpasswordlogin);
            btn_login = FindViewById<Button>(Resource.Id.loginbtnlogin);

            Button btnloginregister = FindViewById<Button>(Resource.Id.loginregister);


            btnloginregister.Click += (sender, e) =>
            {
                Intent RegisterIntent = new Intent(this, typeof(Register));
                StartActivity(RegisterIntent);
            };

            btn_login.Click += (sender, e) =>
            {
                //@insert link
                var request = HttpWebRequest.Create(string.Format(@"https://localhost:5001/api/users"));
                request.ContentType = "application/json";
                request.Method = "GET";
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            var content = reader.ReadToEnd();
                            if (string.IsNullOrWhiteSpace(content))
                            {
                                Console.Out.WriteLine("Response contained empty body...");
                            }
                            else
                            {
                                Console.Out.WriteLine("Response Body: \r\n {0}", content);
                            }

                        }
                    }
                    else
                    {
                        Console.Out.WriteLine("Login successful");
                        Intent MenuIntent = new Intent(this, typeof(Menu));
                        StartActivity(MenuIntent);
                    }
                    
                }
            };

        }
    }
}