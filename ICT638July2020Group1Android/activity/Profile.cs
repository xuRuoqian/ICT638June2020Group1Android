using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using Newtonsoft.Json;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using ICT638July2020Group1Android.Models;
using ICT638July2020Group1Android.models;

namespace ICT638July2020Group1Android   
{
    [Activity(Label = "Profile")]
    public class Profile : Fragment, IOnMapReadyCallback
    {
        private int profileid;
        private TextView txt_fname, txt_lname, txt_pn, txt_address, txt_country;
        private Button btn_share, btn_send, btn_save;
        private User users;
        Agentdetial agent2 = new Agentdetial();
        public Profile(int id)
        {
            profileid = id;
        }
        public string getQuotedString(string str)
        {
            return "\"" + str + "\"";
        }
       public void getProfileDetail()
        {
            string url = "https://10.0.2.2:5001/api/users/"+profileid;
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
                users = JsonConvert.DeserializeObject<User>(result);
            }


        }




        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.MapType = GoogleMap.MapTypeNormal;
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;

            getCurrentLoc(googleMap);
        }

        public async void getCurrentLoc(GoogleMap googleMap)
        {
            Console.WriteLine("Test - CurrentLoc");
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"current Loc - Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    MarkerOptions curLoc = new MarkerOptions();
                    curLoc.SetPosition(new LatLng(location.Latitude, location.Longitude));


                    var address = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = address?.FirstOrDefault();
                    var geocodeAddress = "";
                    if (placemark != null)
                    {
                        geocodeAddress =
                            $"AdminArea:       {placemark.AdminArea}\n" +
                            $"CountryCode:     {placemark.CountryCode}\n" +
                            $"CountryName:     {placemark.CountryName}\n" +
                            $"FeatureName:     {placemark.FeatureName}\n" +
                            $"Locality:        {placemark.Locality}\n" +
                            $"PostalCode:      {placemark.PostalCode}\n" +
                            $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                            $"SubLocality:     {placemark.SubLocality}\n" +
                            $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                            $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    }


                    curLoc.SetTitle("You are here");// + geocodeAddress);
                    curLoc.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure));

                    googleMap.AddMarker(curLoc);


                    CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                    builder.Target(new LatLng(location.Latitude, location.Longitude));
                    builder.Zoom(18);
                    builder.Bearing(155);
                    builder.Tilt(65);

                    CameraPosition cameraPosition = builder.Build();

                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

                    googleMap.MoveCamera(cameraUpdate);
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Toast.MakeText(Activity, "Feature Not Supported", ToastLength.Short);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Toast.MakeText(Activity, "Feature Not Enabled", ToastLength.Short);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Toast.MakeText(Activity, "Needs more permission", ToastLength.Short);
            }
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var mapFrag = MapFragment.NewInstance();// mapOptions);

            ChildFragmentManager.BeginTransaction()
                                    .Add(Resource.Id.profilemap, mapFrag, "map_fragment")
                                    .Commit();

            mapFrag.GetMapAsync(this);



            base.OnCreate(savedInstanceState);
            txt_fname = Activity.FindViewById<TextView>(Resource.Id.txt_profile_fname);
            txt_lname = Activity.FindViewById<TextView>(Resource.Id.txt_profile_lname);
            txt_pn = Activity.FindViewById<TextView>(Resource.Id.txt_profile_phoneNum);
            txt_address = Activity.FindViewById<TextView>(Resource.Id.txt_profile_address);
            txt_country = Activity.FindViewById<TextView>(Resource.Id.txt_profile_country);
            btn_share = Activity.FindViewById<Button>(Resource.Id.btn_profile_share);
            //btn_save = Activity.FindViewById<Button>(Resource.Id.btn_profile_save);
            btn_send = Activity.FindViewById<Button>(Resource.Id.btn_profile_sendMessage);


            getProfileDetail();



            txt_fname.Text = users.Fname.ToString();
            txt_lname.Text = users.Lname.ToString();
            txt_pn.Text = users.PhoneNum.ToString();
            txt_address.Text = users.Address.ToString();
            txt_country.Text = users.Country.ToString();
            // string agpn = agent2.agentphonenumber.ToString();



            /* btn_save.Click += async delegate
             {
                 // if (httpResponse.StatusCode == System.Net.HttpStatusCode.Accepted)
                 // {
                 //   Toast.MakeText(Activity, "Your feedback was saved", ToastLength.Long).Show();
                 // }
                 //否则失败
                 //  else
                 //  {
                 //   Toast.MakeText(Activity, "Your feedback was  not saved", ToastLength.Long).Show();
                 // }
             };*/

            btn_send.Click += async (sender, e) =>
            {
                string recipient = "0212203665";

                try
                {

                    string messagetext = "Hi, please find my contact details as requested. Email " +
                                    txt_address.Text + "Phone Number " + txt_pn.Text;
                    var message = new SmsMessage(messagetext, recipient);


                    await Sms.ComposeAsync(message);
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Sms is not supported on this device.
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                }
            };

            btn_share.Click += async (sender, e) =>
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = "Name: " + txt_fname.Text + txt_lname.Text + "\n" +
                                  "Phone Number" + txt_pn.Text + "\n"
                                  + "Address" + txt_address + "\n"
                                  + "Country" + txt_country + "\n",
                    Title = "Share Text"
                });
            };

        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.profile1, container, false);


            return view;
        }
    }
}
