using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.AnimateSplash
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimateSplash : ContentPage
    {
        public AnimateSplash()
        {
            InitializeComponent();
            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            Device.StartTimer(TimeSpan.FromMilliseconds(5000), () =>
            {
                App.Current.MainPage = new Xamarin.Forms.NavigationPage(new Pages.MasterPage.MasterPage());
              //  Navigation.PushModalAsync(new Pages.MasterPage.MasterPage());
                //if (Helpers.Settings.SubscriptionSettings == "True")
                //{
                //    Navigation.PushModalAsync(new Pages.MasterPage.MasterPage());
                //}
                //else
                //{
                //    Navigation.PushModalAsync(new Pages.AnimateSplash.SubscriptionPage());
                //}
                return false;
            });
        }

        /// <summary>
        /// To desable the device back button...
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}