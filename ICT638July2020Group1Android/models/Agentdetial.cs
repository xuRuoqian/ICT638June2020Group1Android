﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ICT638July2020Group1Android.models
{
    public class Agentdetial
    {
        public int id { get; set; }
        public string name { get; set; }
        public string agentphonenumber { get; set; }
        public string agentaddress { get; set; }
    }
}