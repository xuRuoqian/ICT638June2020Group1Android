﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ICT638July2020Group1Android.models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace ICT638July2020Group1Android.activity
{
    [Activity(Label = "House1")]
    public class House1 : Fragment, IOnMapReadyCallback
    {
        private int houseId;
        private TextView txt_bedroom, txt_bathroom, text_address, txt_rentfees, txt_house1;
        private Button btn_share, btn_sendMessage, btn_map;
        private House house;
        private Agentdetial agent;
        public House1(int id)
        {
            houseId = id;
        }






        public string getQuotedString(string str)
        {
            return "\"" + str + "\"";
        }
        public void getHouseDetail()
        {
            string url = "https://10.0.2.2:5001/api/Houses/" + houseId;
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
                house = JsonConvert.DeserializeObject<House>(result);
            }


        }
        public void getAgentDetail()
        {
            string url = "https://10.0.2.2:5001/api/Agentdetials/" + house.AgentID;
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
                agent = JsonConvert.DeserializeObject<Agentdetial>(result);
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

        public void Btndelete()
        {
            //insert link
            string url = "https://10.0.2.2:5001/api/Houses/5";
            var httpWebRequest = new HttpWebRequest(new Uri(url));
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };
            //httpWebRequest.ServerCertificateCustomValidationCallback = delegate { return true; }
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "DELETE";


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{" +
                    getQuotedString("title") + ":" + getQuotedString("Rachel") + "," +
                    getQuotedString("weeklyRent") + ":" + getQuotedString("Xu") + "," +
                    getQuotedString("numBedrooms") + ":" + getQuotedString("0271234567") + "," +
                    getQuotedString("numBathrooms") + ":" + getQuotedString("38PokapuSt") + "," +
                    getQuotedString("Address") + ":" + getQuotedString("China") + "," +
                    getQuotedString("AgentID") + ":" + getQuotedString("Rachel") +
                               "}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public void Btnadditem()
        {
            //insert link
            string url = "https://10.0.2.2:5001/api/Houses";
            var httpWebRequest = new HttpWebRequest(new Uri(url));
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };
            //httpWebRequest.ServerCertificateCustomValidationCallback = delegate { return true; }
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{" +
                    getQuotedString("title") + ":" + getQuotedString("qwe") + "," +
                    getQuotedString("weeklyRent") + ":" + getQuotedString("212") + "," +
                    getQuotedString("numBedrooms") + ":" + getQuotedString("11") + "," +
                    getQuotedString("numBathrooms") + ":" + getQuotedString("22") + "," +
                    getQuotedString("Address") + ":" + getQuotedString("China") + "," +
                    getQuotedString("AgentID") + ":" + getQuotedString("1") +
                               "}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            var mapFrag = MapFragment.NewInstance();// mapOptions);

            ChildFragmentManager.BeginTransaction()
                                    .Add(Resource.Id.housemap, mapFrag, "map_fragment")
                                    .Commit();

            mapFrag.GetMapAsync(this);

            btn_share = Activity.FindViewById<Button>(Resource.Id.house1share);
            btn_sendMessage = Activity.FindViewById<Button>(Resource.Id.house1sendmessge);
            txt_bedroom = Activity.FindViewById<TextView>(Resource.Id.txthouse1bedroomanswer);
            txt_bathroom = Activity.FindViewById<TextView>(Resource.Id.txthouse1bathroomanswer);
            txt_rentfees = Activity.FindViewById<TextView>(Resource.Id.txthouse1rentfeesanswer);
            text_address = Activity.FindViewById<TextView>(Resource.Id.txthouse1addressanswer);

            txt_house1 = Activity.FindViewById<TextView>(Resource.Id.txthouse1);
            //btn_sendMessage.Click += Btn_sendMessage_Click; 

            Button btndelete = Activity.FindViewById<Button>(Resource.Id.btndelete);


            Button btnadditem = Activity.FindViewById<Button>(Resource.Id.btndelete);

            getHouseDetail();
            getAgentDetail();

            text_address.Text = house.Address.ToString();
            txt_rentfees.Text = house.weeklyRent.ToString();
            txt_bedroom.Text = house.numBedrooms.ToString();
            txt_bathroom.Text = house.numBathrooms.ToString();
            txt_house1.Text = house.title.ToString();
            //string agpn = agent2.agentphonenumber.ToString();

            btndelete.Click += (sender, e) =>
            {
                Btndelete();
            };

            btnadditem.Click += (sender, e) =>
            {
                Btnadditem();
            };




            btn_sendMessage.Click +=  (sender, e) =>
            {
                sendmessageAsync();

            };

            btn_share.Click += async (sender, e) =>
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = "Number of bedrooms: " + txt_bedroom.Text + "\n"
                                       + "Number of bathrooms: " + txt_bathroom.Text + "\n"
                                       + "Rentfees of house: " + txt_rentfees.Text + "\n"
                                       + "Address of house: " + text_address.Text + "\n",
                    Title = txt_house1.Text
                });
            };
        }

        public async System.Threading.Tasks.Task sendmessageAsync()
        {
            try
            {

                string messagetext = "Hi, I am interested in the house at " + text_address.Text + " you have posted for rent."
                                   + "Could I please have more details?";
                var message = new SmsMessage(messagetext, agent.agentphonenumber);//, agpn);


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
        }
            
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.house1, container, false);

            return view;
        }


    }
}
