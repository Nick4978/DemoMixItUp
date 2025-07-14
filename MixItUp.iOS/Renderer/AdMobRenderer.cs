using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Google.MobileAds;
using MixItUp.CustomControls;
using MixItUp.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobRenderer))]
namespace MixItUp.iOS.Renderer
{
    public class AdMobRenderer : ViewRenderer<AdMobView, BannerView>
    {
        string bannerId = "ca-app-pub-8163632011495903/8509887821";
        BannerView adView;
        BannerView CreateNativeAdControl()
        {
            if (adView != null)
                return adView;


            // Setup your BannerView, review AdSizeCons class for more Ad sizes. 
            adView = new BannerView(size: AdSizeCons.SmartBannerPortrait,
                                           origin: new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height))
            {
                AdUnitID = bannerId,
                RootViewController = GetVisibleViewController()
            };

            // Wire AdReceived event to know when the Ad is ready to be displayed
            adView.AdReceived += (object sender, EventArgs e) =>
            {
                //ad has come in
            };


            adView.LoadRequest(GetRequest());
            return adView;
        }

        Request GetRequest()
        {
            var request = Request.GetDefaultRequest();
            // Requests test ads on devices you specify. Your test device ID is printed to the console when
            // an ad request is made. GADBannerView automatically returns test ads when running on a
            // simulator. After you get your device ID, add it here
            //request.TestDevices = new [] { Request.SimulatorId.ToString () };
            request.TestDevices = new[] { "kGADSimulatorID" };
            return request;
        }

        /// 
        /// Gets the visible view controller.
        /// 
        /// The visible view controller.
        UIViewController GetVisibleViewController()
        {
            var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            if (rootController.PresentedViewController == null)
                return rootController;

            if (rootController.PresentedViewController is UINavigationController)
            {
                return ((UINavigationController)rootController.PresentedViewController).VisibleViewController;
            }

            if (rootController.PresentedViewController is UITabBarController)
            {
                return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
            }

            return rootController.PresentedViewController;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
        }


    }
}