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

namespace ICT638July2020Group1Android.activity
{
     class Houseinfo
    {
        public int houseid { get; set; }
        public string bedroom { get; set; }
        public string lavatory { get; set; }
        public string houseaddress { get; set; }
        public string carpark { get; set; }

    }
}