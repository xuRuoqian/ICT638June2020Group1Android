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
    class renthouseAdapter1 : BaseAdapter<Person>
    {
        private List<Person> mitems;
        Context context;

        public renthouseAdapter1(Context context, List<Person> mmitems)
        {
            this.context = context;
            mitems = mmitems;
        }
        public override int Count
        {
            get { return mitems.Count; }
        }


        public override long GetItemId(int position)
        {
            return position;
        }

        public override Person this[int position]
        {
            get { return mitems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;


            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.renthouselist, null, false);
            }
            return row;


        }
    }
}