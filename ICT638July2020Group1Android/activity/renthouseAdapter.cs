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
    public class renthouseAdapter : BaseAdapter
    {

        string[] house;
        
        Activity context;

        public renthouseAdapter(Activity context, string[] items)
        {
            this.context = context;
            house = items;
            
        }



        public override int Count => house.Length;

        

        public override Java.Lang.Object GetItem(int position)
        {
            return house[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.renthouselist, null);
            }
            view.FindViewById<TextView>(Resource.Id.renthouselist).Text = house[position];
            
            
            return view;
        }
    }
}