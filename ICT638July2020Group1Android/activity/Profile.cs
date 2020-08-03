using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ICT638July2020Group1Android.activity
{
    [Activity(Label = "Profile")]
    public class Profile : Activity
    {
        private string recipients;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);



            Button btndesciption_share = FindViewById<Button>(Resource.Id.profile_share);
            btndesciption_share.Click += Desciptionshare_Click;
            Button btndesciption_sms = FindViewById<Button>(Resource.Id.profile_sms);
            btndesciption_sms.Click += Desciptionsms_Click;
            
        }

        private async void Desciptionshare_Click(object sender, EventArgs e)
        {
            TextView tv = FindViewById<TextView>(Resource.Id.profile_text);
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = tv.Text,
                Title = "share Text"
            });
        }

        private async void Desciptionsms_Click(object sender, EventArgs e)
        {
            TextView tv = FindViewById<TextView>(Resource.Id.desciption_text);
            try
            {
                var message = new SmsMessage(tv.Text, recipients);
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

    }
}