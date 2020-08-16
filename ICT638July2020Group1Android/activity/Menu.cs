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
using ICT638July2020Group1Android.Models;
using Newtonsoft.Json;

namespace ICT638July2020Group1Android.activity
{
    [Activity(Label ="Menu")]
    public class Menu : ListActivity

    {
        
        private List<House> House2 = new List<House>();
        private List<User> user2 = new List<User>();
        private List<Agentdetial> agentdetial2 = new List<Agentdetial>();
        int userId;
        public string getQuotedString(string str)
        {
            return "\"" + str + "\"";
        }
        public void getHouseDetail()
        {
            string url = "https://10.0.2.2:5001/api/Houses";
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
                House2 = JsonConvert.DeserializeObject<List<House>>(result);
            }

            
        }

    



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            userId=Intent.GetIntExtra("userId", 1); 
            getHouseDetail();
            String[] item = new string[House2.Count];
            int i = 0;
            foreach (House h in House2)
                item[i++] = h.title;



            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, item);

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Intent house1Intent = new Intent(this, typeof(house1navigation));
                i = args.Position;
                house1Intent.PutExtra("houseId", House2[i].id);
                house1Intent.PutExtra("agentId", House2[i].AgentID);
                house1Intent.PutExtra("userId", userId);
                StartActivity(house1Intent);
            };


        }
    }
}