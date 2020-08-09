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
using ICT638July2020Group1Android.models;
using Newtonsoft.Json;

namespace ICT638July2020Group1Android.activity
{
    [Activity(Label ="Menu"]
    public class Menu : ListActivity

    {
        House houses = new House();



        public string getQuotedString(string str)
        {
            return "\"" + str + "\"";
        }
        public void getHouseDetail()
        {
            string url = "https://localhost:5001/api/Houses";
            var httpWebRequest = new HttpWebRequest(new Uri(url));
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };
            //httpWebRequest.ServerCertificateCustomValidationCallback = delegate { return true; }
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                houses = JsonConvert.DeserializeObject<House>(result);
            }

            
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            House[] items = new House[100];
            String[] names = new string[100];
            for (int i = 0; i < 100; i++)
            {
                if(items[i] != null)
                {
                    names[i] = items[i].title;
                }
            }


            new ArrayAdapter<string>(this, Resource.Layout.menu, names);

            ListView.TextFilterEnabled = true;
            //TextView btnhouse1 = FindViewById<Button>(Resource.Id.textView2inList);

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Intent house1Intent = new Intent(this, typeof(house1navigation));
                StartActivity(house1Intent);
            };


        }
    }
}