using Acr.UserDialogs;
using Microsoft.SqlServer.Management.Smo;
using MixItUp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.UserProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        protected UserProfilePageVM userProfilePageVM;
        string encodedFileAsBase64;
        public UserProfilePage()
        {
            InitializeComponent();
            userProfilePageVM = new UserProfilePageVM(this.Navigation);
            this.BindingContext = userProfilePageVM;
            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralLoginLable))
            {
                lblLogin.Text = Helpers.Settings.GeneralLoginLable;
                userProfilePageVM.Login = Helpers.Settings.GeneralLoginLable;
            }
            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserName))
            {
                userProfilePageVM.UserName = Helpers.Settings.GeneralAuthUserName;
            }
            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserEmail))
            {
                userProfilePageVM.Email = Helpers.Settings.GeneralAuthUserEmail;
            }
            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserProfilePic))
            {
                userProfilePageVM.ProfileImg = Helpers.Settings.GeneralAuthUserProfilePic;
            }

            if (Helpers.Settings.GeneralLoginLable == "Login")
            {
                userProfilePageVM.IsEditProfileImg = false;
            }
            else
            {
                userProfilePageVM.IsEditProfileImg = true;
            }

            MessagingCenter.Subscribe<string>("", "AuthLogin", (sender) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    if (!string.IsNullOrEmpty(Helpers.Settings.GeneralLoginLable))
                    {
                        lblLogin.Text = Helpers.Settings.GeneralLoginLable;
                        userProfilePageVM.Login = Helpers.Settings.GeneralLoginLable;
                    }

                    if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserName))
                    {
                        userProfilePageVM.UserName = Helpers.Settings.GeneralAuthUserName;
                    }

                    if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserEmail))
                    {
                        userProfilePageVM.Email = Helpers.Settings.GeneralAuthUserEmail;
                    }

                    if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserProfilePic))
                    {
                        userProfilePageVM.ProfileImg = Helpers.Settings.GeneralAuthUserProfilePic;
                        string base64 = Utility.Utility.EncodeUrlToBase64(userProfilePageVM.ProfileImg);
                        profileImg.Source = Utility.Utility.GetImageFromBase64(base64);

                        //string url = userProfilePageVM.ProfileImg;
                        //string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(url));
                        //using (var client = new WebClient())
                        //{
                        //    byte[] dataBytes = client.DownloadData(new Uri(url));
                        //    encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                        //} 
                    }

                    if (Helpers.Settings.GeneralLoginLable == "Login")
                    {
                        userProfilePageVM.IsEditProfileImg = false;
                    }
                    else
                    {
                        userProfilePageVM.IsEditProfileImg = true;
                    }
                });
            });
        }

        /// <summary>
        /// To Define the master menu button tapped event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterMenu(object sender, EventArgs e)
        {
            Helpers.Constants.IsUserProfilePage = true;
            App.Current.MainPage = new Pages.MasterPage.MasterPage();
            //App.masterDetailPage.IsPresented = true;
        }

        /// <summary>
        /// To define the login button tapped event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Login_Tapped(object sender, EventArgs e)
        {
            if (Helpers.Settings.GeneralLoginLable == "Login")
            {
                var action = await UserDialogs.Instance.ActionSheetAsync("Login", "CANCEL", null, null, "Facebook", "Google", "Login with Email");
                if (action == "Facebook")
                {
                    DependencyService.Get<IFacebook>().facebook();
                }
                else if (action == "Google")
                {
                    Xamarin.Forms.MessagingCenter.Send<string>("", "GoogleLogin");
                }
                else if (action == "Login with Email")
                {
                    await Navigation.PushModalAsync(new Pages.Login.LoginPage());
                }
            }
            else
            {
                Helpers.Settings.GeneralLoginLable = "Login";
                Helpers.Settings.GeneralSocialLogin = "False";
                Helpers.Settings.GeneralAuthUserName = string.Empty;
                Helpers.Settings.GeneralAuthUserEmail = string.Empty;
                Helpers.Settings.GeneralAuthUserProfilePic = string.Empty;

                userProfilePageVM.UserName = "test user";
                userProfilePageVM.Email = "testuser@gmail.com";
                userProfilePageVM.ProfileImg = "profile_noimage.png";
                profileImg.Source = "profile_noimage.png";


                Xamarin.Forms.MessagingCenter.Send<string>("", "AuthLogin");
            }
        }
    }
}