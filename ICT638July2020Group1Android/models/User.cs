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

namespace ICT638July2020Group1Android.Models
{
    class User
    {
        public int id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
