using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MixItUp.iOS.Dependency
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