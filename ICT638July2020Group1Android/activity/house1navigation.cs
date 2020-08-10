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

namespace ICT638July2020Group1Android
{
    [Activity(Label = "house1navigation")]
    public class house1navigation : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.navgation);


            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.house1navigation);
            navigation.SetOnNavigationItemSelectedListener(this);



        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            House house = new House();
            House1 house1testFragment = new House1(house.id);
            Agentdetial agid = new Agentdetial();
            Agent agenttestFragment = new Agent(agid.id);
            User usid = new User();
            Profile protestFragment = new Profile(usid.id);


            FrameLayout fragCon = FindViewById<FrameLayout>(Resource.Id.fragContainer);
            FragmentTransaction transaction;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home_24dp:              
                    fragCon.RemoveAllViewsInLayout();
                    fragCon.RemoveViews(0, fragCon.ChildCount);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, house1testFragment);                   
                    transaction.Commit();
                    return true;
                case Resource.Id.navigation_agent_24dp:
                    fragCon.RemoveAllViewsInLayout();
                    fragCon.RemoveViews(0, fragCon.ChildCount);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, agenttestFragment);                   
                    transaction.Commit();
                    return true;
                case Resource.Id.navigation_profile_24dp:
                    fragCon.RemoveAllViewsInLayout();
                    fragCon.RemoveViews(0, fragCon.ChildCount);
                    transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.fragContainer, protestFragment);
                    transaction.Commit();
                    return true;
            }
            return false;
        }
    }
}