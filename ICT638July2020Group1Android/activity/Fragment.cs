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

namespace ICT638July2020Group1Android.activity
{

    public class Fagment : Fragment
    {
        View rootView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            if (container == null)
            {
                return null;
            }
            if (rootView != null)
            {
                ViewGroup parent = (ViewGroup)rootView.Parent;
                parent.RemoveView(rootView);
            }
            else
            {
                rootView = inflater.Inflate(Resource.Layout.fragment, container, false);
            }
            return rootView;
        }
    }
}