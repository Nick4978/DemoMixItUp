using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Platform;
using Acr.UserDialogs;
using Xamarin.Forms;
using MixItUp.GoogleAuthentication;
using MixItUp.Droid.GoogleAuth;
using Plugin.Permissions;
using Android.Gms.Ads;
using System.Threading.Tasks;
using Android.Provider;

namespace MixItUp.Droid
{
    [Activity(Label = "MixItUp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IGoogleAuthenticationDelegate
    {

        //TODO : To declare variables for Google Auth.
        public static GoogleAuthenticator Auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            ImageCircleRenderer.Init();
            UserDialogs.Init(this);

            MobileAds.Initialize(ApplicationContext, "ca-app-pub-8163632011495903/5496711675");

            Auth = new GoogleAuthenticator(Configuration.ClientId, Configuration.Scope, Configuration.RedirectUrl, this);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            XamEffects.Droid.Effects.Init();

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

            //Xamarin.Forms.CarouselView.and
            //  CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();


            CopyAsset("cabinetIngredients.json");
            CopyAsset("cabinets.json");
            CopyAsset("categories.json");
            CopyAsset("garnishes.json");
            CopyAsset("ingredients.json");
            CopyAsset("instructions.json");
            CopyAsset("recipeCategories.json");
            CopyAsset("recipeIngredient.json");
            CopyAsset("recipes.json");
            CopyAsset("reviews.json");
            CopyAsset("nutrition.json");
            CopyAsset("Barfriender.db");

            LoadApplication(new App());

            // Google Login...
            MessagingCenter.Subscribe<string>(this, "GoogleLogin", (sender) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    GmailAuthMetod();
                });
            });
        }

        private void CopyAsset(string assetFile)
        {
            var fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), assetFile);
            if (Exists(fileName))
            {
                DeleteFile(fileName);
            }
            if (!Exists(fileName))
            {
                using (var source = Assets.Open($@"{assetFile}"))
                using (var dest = OpenFileOutput(assetFile, FileCreationMode.Private))
                {
                    source.CopyTo(dest);
                }
            }
        }

        private bool Exists(string source)
        {
            Context context = Android.App.Application.Context;
            Java.IO.File file = new Java.IO.File(source);

            string where = MediaStore.MediaColumns.Data + "=?";
            string[] selectionArgs = new string[] { file.AbsolutePath };
            ContentResolver contentResolver = context.ContentResolver;
            Android.Net.Uri filesUri = MediaStore.Files.GetContentUri("external");
            return file.Exists();
        }

        public void DeleteFile(string source)
        {
            Context context = Android.App.Application.Context;
            Java.IO.File file = new Java.IO.File(source);

            string where = MediaStore.MediaColumns.Data + "=?";
            string[] selectionArgs = new string[] { file.AbsolutePath };
            ContentResolver contentResolver = context.ContentResolver;
            Android.Net.Uri filesUri = MediaStore.Files.GetContentUri("external");
            
            if (file.Exists())
            {
                var retI = contentResolver.Delete(filesUri, where, selectionArgs);
                var ret = file.Delete();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region Google Auth

        public async void GmailAuthMetod()
        {
            await Task.Delay(2000);
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var authenticator = Auth.GetAuthenticator();
                    var intent = authenticator.GetUI(this);
                    StartActivity(intent);
                }
                catch (Exception ex)
                {
                }
            });
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            // Retrieve the user's email address
            var googleService = new GoogleService();
            var email = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);
            System.Diagnostics.Debug.WriteLine("email -------------**************************-----------------" + email);
            Device.BeginInvokeOnMainThread(async () =>
            {
                Xamarin.Forms.MessagingCenter.Send<string>("", "AuthLogin");
            });
        }

        public void OnAuthenticationCanceled()
        {
            //new AlertDialog.Builder(this)
            //               .SetTitle("Authentication canceled")
            //               .SetMessage("You didn't completed the authentication process")
            //               .Show();
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                new AlertDialog.Builder(this)
                           .SetTitle(message)
                           .SetMessage(exception?.ToString())
                           .Show();
            });
        }

        #endregion
    }
}