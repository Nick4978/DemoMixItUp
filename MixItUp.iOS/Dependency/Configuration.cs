using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MixItUp.iOS.Dependency
{
    public static class Configuration
    {
        public const string ClientId = "920934912970-5ndom1quhij4ij1dv37l5gpndsv9qvn8.apps.googleusercontent.com";
        //  public const string ClientId = "1070737727543-r6ooiempudbopban6vls62cur4sveutq.apps.googleusercontent.com";
        public const string Scope = "openid email profile"; 
       // public const string Scope = "email"; 
        //public const string Scope = "https://www.googleapis.com/auth/userinfo.email"; 
        public const string RedirectUrl = "com.teracorp.mixitup:/oauth2redirect";
    }
}