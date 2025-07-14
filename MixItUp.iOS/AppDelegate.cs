using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using CarouselView.FormsPlugin.iOS;
using Xamarin.Forms;
using MixItUp.GoogleAuthentication;
using MixItUp.iOS.Dependency;

using Newtonsoft.Json;

namespace MixItUp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IGoogleAuthenticationDelegate
    {

        private readonly JsonSerializerSettings _serializerSettings;
        public static UIViewController controller;
        public static GoogleAuthenticator Auth;
        public UIViewController vc;
        public override UIWindow Window
        {
            get;
            set;
        }
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            try
            {
                global::Xamarin.Forms.Forms.Init();
                Google.MobileAds.MobileAds.Configure("ca-app-pub-8163632011495903~6075296175");
                ImageCircleRenderer.Init();
                //FFImageLoading

                global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();

                FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
                Plugin.InputKit.Platforms.iOS.Config.Init();
                XamEffects.iOS.Effects.Init();
                CarouselViewRenderer.Init();

                var path = CopyAsset("Barfriender.db");
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

                LoadApplication(new App(path));

                // Google Login...
                MessagingCenter.Subscribe<string>(this, "GoogleLogin", (sender) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Auth = new GoogleAuthenticator(Configuration.ClientId, Configuration.Scope, Configuration.RedirectUrl, this);
                        var authenticator = Auth.GetAuthenticator();
                        vc = authenticator.GetUI();
                        App.VarGmail = true;
                        var window = UIApplication.SharedApplication.KeyWindow;
                        var currvc = window.RootViewController;
                        while (currvc.PresentedViewController != null)
                        {
                            currvc = currvc.PresentedViewController;
                        }
                        currvc.PresentViewController(vc, true, null);
                    });
                });

                return base.FinishedLaunching(app, options);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string CopyAsset(string fileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = docFolder; //Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string dbPath = Path.Combine(libFolder, fileName);

            if (!File.Exists(dbPath))
            {
                var existingDb = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
                File.Copy(existingDb, dbPath);
            }

            return dbPath;
        }

        #region Google

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            App.VarGmail = false;
            // SFSafariViewController doesn't dismiss itself
            vc.DismissViewController(true, null);
            var googleService = new GoogleService();
            var email = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);
            System.Diagnostics.Debug.WriteLine("email -------------**************************-----------------" + email);
            Device.BeginInvokeOnMainThread(async () =>
            {
                token = null;
                Xamarin.Forms.MessagingCenter.Send<string>("", "AuthLogin");
                //if (Helpers.Constants.IsProfilePage == false)
                //    await App.NavigationToProfile();
                //else
                //    Xamarin.Forms.MessagingCenter.Send<string>(Convert.ToString(Helpers.Constants.isAuthLogin), "ProfileGoogleAuthLogin");
            });
        }

        public void OnAuthenticationCanceled()
        {
            // SFSafariViewController doesn't dismiss itself
            vc.DismissViewController(true, null);
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            // SFSafariViewController doesn't dismiss itself
            vc.DismissViewController(true, null);

            var alertController = new UIAlertController
            {
                Title = message,
                Message = exception?.ToString()
            };
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alertController, true, null);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            Window = UIApplication.SharedApplication.KeyWindow;
            var uri_netfx = new Uri(url.AbsoluteString);
            Auth?.OnPageLoading(uri_netfx);
            return true;
        }
        #endregion



        /*        private string CopyAsset(string assetFile)
                {
                    try
                    {
         
                        string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

                        if (!Directory.Exists(libFolder))
                        {
                            Directory.CreateDirectory(libFolder);
                        }
                        string path = Path.Combine(libFolder, assetFile);

                        // This is where we copy in the pre-created database
                        if (!File.Exists(path))
                        {
                            var file = Path.GetFileNameWithoutExtension(assetFile);
                            var existingDb = NSBundle.MainBundle.PathForResource(file, "db");
                            File.Copy(existingDb, path);
                            if (!File.Exists(path))
                            {
                                var a = 1;
                            }
                        }

                        return path;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }*/
    }
}
