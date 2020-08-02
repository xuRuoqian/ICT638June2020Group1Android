using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace ICT638July2020Group1Android.activity
{
    [Activity(Label = "navigation")]
    public class Navigation : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Fagment testFragment = new Fagment();

            FrameLayout fragCon = FindViewById<FrameLayout>(Resource.Id.fragContainer);
            FragmentTransaction transaction;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    textMessage.SetText(Resource.String.title_home);
                    fragCon.RemoveAllViewsInLayout();
                    fragCon.RemoveViews(0, fragCon.ChildCount);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, testFragment, "Home");
                    transaction.AddToBackStack("Home");
                    transaction.Commit();

                    return true;
                case Resource.Id.navigation_items:
                    textMessage.SetText(Resource.String.title_items);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, testFragment, "Items");
                    transaction.AddToBackStack("Items");
                    transaction.Commit();
                    return true;
                case Resource.Id.navigation_location:
                    textMessage.SetText(Resource.String.title_location);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, testFragment, "Map");
                    transaction.AddToBackStack("Map");
                    transaction.Commit();
                    return true;
                case Resource.Id.navigation_profile:
                    textMessage.SetText(Resource.String.title_profile);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, testFragment, "Profile");
                    transaction.AddToBackStack("Profile");
                    transaction.Commit();
                    return true;
                case Resource.Id.navigation_stat_button:
                    textMessage.SetText(Resource.String.title_stat_button);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, testFragment, "Notifications");
                    transaction.AddToBackStack("Notifications");
                    transaction.Commit();
                    return true;
            }
            return false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Navigation navigation1 = this;
            Xamarin.Essentials.Platform.Init(navigation1, savedInstanceState);
            SetContentView(Resource.Layout.navigation_layout);

            textMessage = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView bNavView = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            bNavView.SetOnNavigationItemSelectedListener(this);

        }
    }
}