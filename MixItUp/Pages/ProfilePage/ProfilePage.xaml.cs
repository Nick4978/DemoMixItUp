using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixItUp.Model;
using MixItUp.Pages.CreateProfile;
using MixItUp.Pages.UserProfile;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.ProfilePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        //TODO : To Declare Class Level Variables...
        protected ProfilePageVM _ProfilePageVM;

        public ProfilePage()
        {
            InitializeComponent();
            //_ProfilePageVM = new ProfilePageVM(this.Navigation);
            //this.BindingContext = _ProfilePageVM;

            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            if (Helpers.Constants.IsCartPage == true)
            {
               

                ImgMyCartOrng.IsVisible = true;
                LblMyCartOrng.IsVisible = true;
                ImgMyCartBlack.IsVisible = false;
                LblMyCartBlack.IsVisible = false;

                ImgCocktailOrng.IsVisible = false;
                LblCocktailOrng.IsVisible = false;
                ImgCocktailBlack.IsVisible = true;
                LblCocktailBlack.IsVisible = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }

        private async void Cocktails_Clicked(object sender, EventArgs e)
        {
            App.masterDetailPage.Detail = new Pages.RecipePage.RecipePage();
            App.masterDetailPage.IsPresented = false;
            //await Navigation.PushModalAsync(new Pages.UserProfile.UserProfilePage());
        }

        private async void MyProfile_Clicked(object sender, EventArgs e)
        {
            if (Settings.ProfilePresent)
                App.masterDetailPage.Detail = new CreateProfilePage();
            else
            {
                App.masterDetailPage.Detail = new UserProfilePage();
            }
            App.masterDetailPage.IsPresented = false;
            //await Navigation.PushModalAsync(new Pages.CreateProfile.CreateProfilePage());
        }

        private void MyCart_Clicked(object sender, EventArgs e)
        {
            App.masterDetailPage.Detail = new Pages.CartPage.CartPage();
            App.masterDetailPage.IsPresented = false;
        }

        private void MyCabinets_Clicked(object sender, EventArgs e)
        {

            App.masterDetailPage.Detail = new Pages.Cabinet.CabinetPage();
            App.masterDetailPage.IsPresented = false;
        }

        private void RecipedetailClicked(object sender, EventArgs e)
        {
            //App.masterDetailPage.Detail = new Pages.RecipeDetail.RecipeDetailPage();
            App.masterDetailPage.IsPresented = false;
        }

        private void Presentation_Clicked(object sender, EventArgs e)
        {
            App.masterDetailPage.Detail = new Pages.RecipePage.NewRecipePage();
            App.masterDetailPage.IsPresented = false;
        }
    }
}