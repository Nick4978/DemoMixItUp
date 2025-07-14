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

namespace MixItUp.Droid.Dependency
{
    public class Data
    {
        public int height { get; set; }
        public bool is_silhouette { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }

    public class FacebookUserModel
    {
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public Picture picture { get; set; }
        public string id { get; set; }
    }
}