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
        static readonly string[] mitems = new String[] {
        "Albany10","piha5","city1","city2"
        };




        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            ListAdapter = new renthouseAdapter1(this, mitems);


            //ListView mlistview = FindViewById<ListView>(Resource.Id.mylistview);
            //mlistview.ItemClick += Mlistview_Click;
            // mlistview.Adapter = adpter;

            // new ArrayAdapter<string>(this, Resource.Layout.renthouselist, houses);
            // new ArrayAdapter<Person>(this, Resource.Layout.renthouselist, (IList<Person>)mItems);
            ListView.TextFilterEnabled = true;
 
        }

      private void Mlistview_Click(object sender, AdapterView.ItemClickEventArgs e)
      {
            SetContentView(Resource.Layout.register_a);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}