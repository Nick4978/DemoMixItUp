using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Foundation;
using MixItUp.Interface;
using MixItUp.iOS.Dependency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(FacebookAuth))]
namespace MixItUp.iOS.Dependency
{
    public class FacebookAuth : IFacebook
    {
        public UIViewController vc;

        public void facebook()
        {
            var auth = new OAuth2Authenticator(
                                           clientId: "769200510551179",
                                           scope: "email",
                                           authorizeUrl: new System.Uri("https://m.facebook.com/dialog/oauth"),
                                           redirectUrl: new System.Uri("https://www.facebook.com/connect/login_success.html"),
                                           isUsingNativeUI: false);

            auth.Completed += Auth_Completed;
            vc = new UIViewController();
            vc = auth.GetUI();
            var window = UIApplication.SharedApplication.KeyWindow;
            var currvc = window.RootViewController;
            while (currvc.PresentedViewController != null)
            {
                currvc = currvc.PresentedViewController;
            }
            auth.ShowErrors = false;
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(auth);
            //vc.PresentViewController(auth.GetUI(), true, null);
        }

        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request(
                                                "GET",
                                                new System.Uri("https://graph.facebook.com/me?fields=email,name,first_name,last_name,gender,picture"),
                                                null,
                                                e.Account);

                var fbResponse = await request.GetResponseAsync();
                var obj = JObject.Parse(fbResponse.GetResponseText());
                var email = obj["email"].ToString().Replace("\"", "");
                var FacebookAccessToken = e.Account.Properties["access_token"];
                var json = fbResponse.GetResponseText();
                var fbUser = JsonConvert.DeserializeObject<FacebookUserModel>(json);
                var name = fbUser.name;
                Helpers.Constants.AuthUserEmail = email;
                Helpers.Constants.AuthUserFName = fbUser.first_name;
                Helpers.Constants.AuthUserLName = fbUser.last_name;
                Helpers.Constants.AuthUserPhone = string.Empty;
                Helpers.Constants.AuthUserProfilePic = fbUser.picture.data.url;

                Helpers.Settings.GeneralToken = FacebookAccessToken;
                Helpers.Settings.GeneralAuthUserEmail = email;
                Helpers.Settings.GeneralAuthUserName = fbUser.first_name + " " + fbUser.last_name;
                Helpers.Settings.GeneralAuthUserProfilePic = fbUser.picture.data.url;
                Helpers.Settings.GeneralLoginLable = "Logout";
                Helpers.Settings.GeneralSocialLogin = "True";


                Xamarin.Forms.MessagingCenter.Send<string>("", "AuthLogin");
            }
        }
    }
}