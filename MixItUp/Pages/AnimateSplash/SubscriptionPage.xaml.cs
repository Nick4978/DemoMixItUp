using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.AnimateSplash
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubscriptionPage : ContentPage
    {
        public SubscriptionPage()
        {
            InitializeComponent();

            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        private async void FreeTrial_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Pages.MasterPage.MasterPage());
        }

        private async void Subscription_Clicked(object sender, EventArgs e)
        {
            Helpers.Settings.SubscriptionSettings = "True";
            await Navigation.PushModalAsync(new Pages.MasterPage.MasterPage());
        }
    }
}