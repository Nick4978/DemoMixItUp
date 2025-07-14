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

namespace MixItUp.Droid.GoogleAuth
{
    public static class Configuration
    {
        public const string ClientId = "920934912970-38ku0m8m0jsl00lnuc91krfvu0pa8m08.apps.googleusercontent.com";
        //public const string ClientId = "1070737727543-1to4ha56u4m8bdg21o7ldgeua455286e.apps.googleusercontent.com";
        //for ios [972828221142-28r9jccg8j5mj3s7tdpvne4j0k35n7pe.apps.googleusercontent.com]
        public const string Scope = "openid email profile";
        // public const string RedirectUrl = "com.teracorpinc.mixitup:/oauth2redirect";
        public const string RedirectUrl = "com.teracorpinc.mixitup:/oauth2redirect";
      //  public const string RedirectUrl = "com.asiacab.passenger:/oauth2redirect";
    }
}