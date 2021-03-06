﻿using System;
using System.Linq;
using System.Net;
using System.IO;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Xamarin.Essentials;
using ICT638July2020Group1Android.models;
using Newtonsoft.Json;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using ICT638July2020Group1Android.Models;

namespace ICT638July2020Group1Android

{
    [Activity(Label = "agentActivity")]

    public class Agent : Fragment, IOnMapReadyCallback
    {
        private User user1;
        private int agentid;
        //private TextView txt_Fname, txt_Lname;
        private Agentdetial agent1;
        private TextView txt_name, txt_emailAddress, txt_phoneNum;
        
        private Button btn_share, btn_sendMessage;
        public Agent(int id)
        {
            agentid = id;
        }

        public string getQuotedString(string str)
        {
            return "\"" + str + "\"";
        }
        public void getAgentDetail()
        {
            string url = "https://10.0.2.2:5001/api/Agentdetials/"+agentid;
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
                agent1 = JsonConvert.DeserializeObject<Agentdetial>(result);
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

        public void Btnupdateagent()
        {
            string url = "https://10.0.2.2:5001/api/agentdetials";
            var httpWebRequest = new HttpWebRequest(new Uri(url));
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };
            //httpWebRequest.ServerCertificateCustomValidationCallback = delegate { return true; }
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{" +
                    getQuotedString("name") + ":" + getQuotedString("qwe") + "," +
                    getQuotedString("agentphonenumber") + ":" + getQuotedString("123") + "," +
                    getQuotedString("agentaddress") + ":" + getQuotedString("qwerr") +
                               "}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }




        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }


       
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var mapFrag = MapFragment.NewInstance();// mapOptions);

            ChildFragmentManager.BeginTransaction()
                                    .Add(Resource.Id.agentmap, mapFrag, "map_fragment")
                                    .Commit();

            mapFrag.GetMapAsync(this);

           
            btn_share = Activity.FindViewById<Button>(Resource.Id.btn_agent_share);

            btn_sendMessage = Activity.FindViewById<Button>(Resource.Id.btn_agent_sendMessage);
            txt_name = Activity.FindViewById<TextView>(Resource.Id.txt_agent_name);
            txt_emailAddress = Activity.FindViewById<TextView>(Resource.Id.txt_agent_emailAddress);
            txt_phoneNum = Activity.FindViewById<TextView>(Resource.Id.txt_agent_PhoneNum);

            Button btnupdateagent = Activity.FindViewById<Button>(Resource.Id.btnupdateagent);

           // TextView userFname = Activity.FindViewById<TextView>(Resource.Id.txt_profile_fname);
           // TextView userLname = Activity.FindViewById<TextView>(Resource.Id.txt_profile_lname);

            getAgentDetail();

            txt_name.Text = agent1.name.ToString();
            txt_emailAddress.Text = agent1.agentaddress.ToString();
            txt_phoneNum.Text = agent1.agentphonenumber.ToString();
           // txt_Fname.Text = userFname.Text;
          //  txt_Lname.Text = userLname.Text;


            btnupdateagent.Click += (sender, e) =>
            {
                Btnupdateagent();
            };





            btn_share.Click += (sender, e) =>
            {

                string description = "Name: " + txt_name.Text + "\n"
                                       + "emailAddress: " + txt_emailAddress.Text + "\n"
                                       + "Phone Number: " + txt_phoneNum.Text + "\n";

                ShareText(description);
            };

            btn_sendMessage.Click += async (sender, e) =>
            {
                string userFname = "Haoli.";
                string userLname = "Lu";
                try
                {
                    
                    string messagetext = "Hi, I am " + userFname + userLname + " saw your details on the Rent-a-go app, "
                                      + "Could you please send me details of more houses for rent in the same price range?";
                    var message = new SmsMessage(messagetext, txt_phoneNum.Text);// 


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

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.agent1, container, false);
            return view;
        }
    }
}