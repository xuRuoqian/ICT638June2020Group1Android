using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ICT638July2020Group1Android.activity;
using ICT638July2020Group1Android.models;
using ICT638July2020Group1Android.Models;

namespace ICT638July2020Group1Android
{
    [Activity(Label = "house1navigation")]
    public class house1navigation : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {

        int houseId;
        int agentId;
        int userId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.navgation);

            houseId = Intent.GetIntExtra("houseId", 1);
            agentId = Intent.GetIntExtra("agentId", 1);
            userId = Intent.GetIntExtra("userId", 1);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.house1navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            navigation.SelectedItemId = Resource.Id.navigation_home;


        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            
            House1 house1testFragment = new House1(houseId);
            Agent agenttestFragment = new Agent(agentId);           
            Profile protestFragment = new Profile(userId);


            FrameLayout fragCon = FindViewById<FrameLayout>(Resource.Id.fragContainer);
            FragmentTransaction transaction;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:              
                    fragCon.RemoveAllViewsInLayout();
                    fragCon.RemoveViews(0, fragCon.ChildCount);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, house1testFragment);
                    transaction.AddToBackStack("house");
                    transaction.Commit();
                    return true;
                case Resource.Id.navigation_agent:
                    fragCon.RemoveAllViewsInLayout();
                    fragCon.RemoveViews(0, fragCon.ChildCount);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, agenttestFragment);
                    transaction.AddToBackStack("agent");
                    transaction.Commit();
                    return true;
                case Resource.Id.navigation_profile:
                    fragCon.RemoveAllViewsInLayout();
                    fragCon.RemoveViews(0, fragCon.ChildCount);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, protestFragment);
                    transaction.AddToBackStack("user");
                    transaction.Commit();
                    return true;
            }
            return false;
        }
    }
}