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
using ICT638July2020Group1Android.activity;

namespace ICT638July2020Group1Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    
    public class houselist1 : ListActivity
    {
        static readonly string[] houses = new String[] {
   "Albany10bedroom3","piha5bedroom3","city1bedroom3","city2bedroom3","city3bedroom3",
    "city5bedroom3","city6bedroom3","Epsom1bedroom3","Moun tAlbert1bedroom3","Mount Roskill2bedroom3"
    };
        private List<Android.App.Person> mItems;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            mItems = new List<Android.App.Person>();
            mItems.Add(new Houseinfo() { bedroom = "4", lavatory = "2", houseaddress = "36B Shelter Dr", carpark = "4" });
            mItems.Add(new Houseinfo() { bedroom = "3", lavatory = "3", houseaddress = "37B Shelter Dr", carpark = "3" });
            mItems.Add(new Houseinfo() { bedroom = "2", lavatory = "5", houseaddress = "34B city Dr", carpark = "2" });
            mItems.Add(new Houseinfo() { bedroom = "3", lavatory = "1", houseaddress = "20B city Dr", carpark = "4" });
            mItems.Add(new Houseinfo() { bedroom = "5", lavatory = "2", houseaddress = "15B new lynn Dr", carpark = "1" });
            mItems.Add(new Houseinfo() { bedroom = "6", lavatory = "3", houseaddress = "16B Mount albert Dr", carpark = "1" });
            mItems.Add(new Houseinfo() { bedroom = "1", lavatory = "3", houseaddress = "36B Albany Dr", carpark = "1" });
            mItems.Add(new Houseinfo() { bedroom = "2", lavatory = "2", houseaddress = "17B Greenhite Dr", carpark = "2" });
            mItems.Add(new Houseinfo() { bedroom = "4", lavatory = "1", houseaddress = "18B Wo Dr", carpark = "2" });
            mItems.Add(new Houseinfo() { bedroom = "2", lavatory = "1", houseaddress = "19B Tian Dr", carpark = "2" });

            ListAdapter = new renthouseAdapter(this, houses, mItems);
           
            new ArrayAdapter<string>(this, Resource.Layout.renthouselist, houses);
            new ArrayAdapter<Android.App.Person>(this, Resource.Layout.renthouselist, mItems);
            ListView.TextFilterEnabled = true;

            //ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
           // {
             //   Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            //};

           
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}