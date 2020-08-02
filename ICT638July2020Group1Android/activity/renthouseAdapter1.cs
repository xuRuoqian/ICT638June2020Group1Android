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
    class renthouseAdapter1 : BaseAdapter
    {
        string [] mitems;
        private List<houseinfo> mItems;
        Activity context;
        private houselist1 houselist1;

        public renthouseAdapter1(Activity context, string[] mmitems)
        {
            this.context = context;
            mitems = mmitems;
        }

        public renthouseAdapter1(houselist1 houselist1, List<houseinfo> mItems)
        {
            this.houselist1 = houselist1;
            this.mItems = mItems;
        }

        public override int Count
        {
            get { return mitems.Length; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return mitems[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;


            if (row == null)
            {
                row = context.LayoutInflater.Inflate(Resource.Layout.renthouselist, null);
            }
            row.FindViewById<TextView>(Resource.Id.renthouselist).Text = mitems[position];


            return row;

        }
    }
}