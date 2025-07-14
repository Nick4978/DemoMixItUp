using Acr.UserDialogs;
using MixItUp.Interface;
using MixItUp.Model;
using MixItUp.Pages.CreateProfile;
using MixItUp.Pages.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamEffects;

namespace MixItUp.Pages.MasterPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                grdPresentation.IsVisible = true;
            }

            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserName))
            {
                lblUserName.Text = Helpers.Settings.GeneralAuthUserName;
            }
            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserProfilePic))
            {
                ImgProfilePic.Source = Helpers.Settings.GeneralAuthUserProfilePic;
            }
            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralLoginLable))
            {
                lblLogin.Text = Helpers.Settings.GeneralLoginLable;
            }

            MessagingCenter.Subscribe<string>("", "AuthLogin", (sender) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    if (!string.IsNullOrEmpty(Helpers.Settings.GeneralLoginLable))
                    {
                        lblLogin.Text = Helpers.Settings.GeneralLoginLable;
                    }

                    if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserName))
                    {
                        lblUserName.Text = Helpers.Settings.GeneralAuthUserName;
                    }

                    if (!string.IsNullOrEmpty(Helpers.Settings.GeneralAuthUserProfilePic))
                    {
                        ImgProfilePic.Source = Helpers.Settings.GeneralAuthUserProfilePic;
                    }
                });
            });

            if (Helpers.Constants.IsRecipePage == true)
            {
                Helpers.Constants.IsRecipePage = false;
                Detail = new NavigationPage(new Pages.RecipePage.RecipePage());
                IsPresented = true;
            }
            else if (Helpers.Constants.IsCartPage == true)
            {
                Helpers.Constants.IsCartPage = false;
                Detail = new NavigationPage(new Pages.CartPage.CartPage());
                IsPresented = true;

                ImgMyCartOrng.IsVisible = true;
                LblMyCartOrng.IsVisible = true;
                ImgMyCartBlack.IsVisible = false;
                LblMyCartBlack.IsVisible = false;

                ImgCocktailOrng.IsVisible = false;
                LblCocktailOrng.IsVisible = false;
                ImgCocktailBlack.IsVisible = true;
                LblCocktailBlack.IsVisible = true;

                ImgCabinetOrng.IsVisible = false;
                LblCabinetOrng.IsVisible = false;
                ImgCabinetBlack.IsVisible = true;
                LblCabinetBlack.IsVisible = true;

                ImgProfileOrng.IsVisible = false;
                LblProfileOrng.IsVisible = false;
                ImgProfileBlack.IsVisible = true;
                LblProfileBlack.IsVisible = true;

                ImgPresentationOrng.IsVisible = false;
                LblPresentationOrng.IsVisible = false;
                ImgPresentationBlack.IsVisible = true;
                LblPresentationBlack.IsVisible = true;

                ImgMyRecipesOrng.IsVisible = false;
                LblMyRecipesOrng.IsVisible = false;
                ImgMyRecipesBlack.IsVisible = true;
                LblMyRecipesBlack.IsVisible = true;
            }
            else if (Helpers.Constants.IsCabinetPage == true)
            {
                Helpers.Constants.IsCabinetPage = false;
                Detail = new NavigationPage(new Pages.Cabinet.CabinetPage());
                IsPresented = true;

                ImgCabinetOrng.IsVisible = true;
                LblCabinetOrng.IsVisible = true;
                ImgCabinetBlack.IsVisible = false;
                LblCabinetBlack.IsVisible = false;

                ImgMyCartOrng.IsVisible = false;
                LblMyCartOrng.IsVisible = false;
                ImgMyCartBlack.IsVisible = true;
                LblMyCartBlack.IsVisible = true;

                ImgCocktailOrng.IsVisible = false;
                LblCocktailOrng.IsVisible = false;
                ImgCocktailBlack.IsVisible = true;
                LblCocktailBlack.IsVisible = true;

                ImgProfileOrng.IsVisible = false;
                LblProfileOrng.IsVisible = false;
                ImgProfileBlack.IsVisible = true;
                LblProfileBlack.IsVisible = true;

                ImgPresentationOrng.IsVisible = false;
                LblPresentationOrng.IsVisible = false;
                ImgPresentationBlack.IsVisible = true;
                LblPresentationBlack.IsVisible = true;

                ImgMyRecipesOrng.IsVisible = false;
                LblMyRecipesOrng.IsVisible = false;
                ImgMyRecipesBlack.IsVisible = true;
                LblMyRecipesBlack.IsVisible = true;
            }
            else if (Helpers.Constants.IsUserProfilePage == true)
            {
                Helpers.Constants.IsUserProfilePage = false;
                Detail = new NavigationPage(new Pages.UserProfile.UserProfilePage());
                IsPresented = true;

                ImgProfileOrng.IsVisible = true;
                LblProfileOrng.IsVisible = true;
                ImgProfileBlack.IsVisible = false;
                LblProfileBlack.IsVisible = false;

                ImgMyCartOrng.IsVisible = false;
                LblMyCartOrng.IsVisible = false;
                ImgMyCartBlack.IsVisible = true;
                LblMyCartBlack.IsVisible = true;

                ImgCocktailOrng.IsVisible = false;
                LblCocktailOrng.IsVisible = false;
                ImgCocktailBlack.IsVisible = true;
                LblCocktailBlack.IsVisible = true;

                ImgCabinetOrng.IsVisible = false;
                LblCabinetOrng.IsVisible = false;
                ImgCabinetBlack.IsVisible = true;
                LblCabinetBlack.IsVisible = true;

                ImgPresentationOrng.IsVisible = false;
                LblPresentationOrng.IsVisible = false;
                ImgPresentationBlack.IsVisible = true;
                LblPresentationBlack.IsVisible = true;

                ImgMyRecipesOrng.IsVisible = false;
                LblMyRecipesOrng.IsVisible = false;
                ImgMyRecipesBlack.IsVisible = true;
                LblMyRecipesBlack.IsVisible = true;
            }
            else if (Helpers.Constants.IsNewRecipePage == true)
            {
                Helpers.Constants.IsNewRecipePage = false;
                Detail = new NavigationPage(new Pages.RecipePage.NewRecipePage());
                IsPresented = true;

                ImgPresentationOrng.IsVisible = true;
                LblPresentationOrng.IsVisible = true;
                ImgPresentationBlack.IsVisible = false;
                LblPresentationBlack.IsVisible = false;

                ImgProfileOrng.IsVisible = false;
                LblProfileOrng.IsVisible = false;
                ImgProfileBlack.IsVisible = true;
                LblProfileBlack.IsVisible = true;

                ImgMyCartOrng.IsVisible = false;
                LblMyCartOrng.IsVisible = false;
                ImgMyCartBlack.IsVisible = true;
                LblMyCartBlack.IsVisible = true;

                ImgCocktailOrng.IsVisible = false;
                LblCocktailOrng.IsVisible = false;
                ImgCocktailBlack.IsVisible = true;
                LblCocktailBlack.IsVisible = true;

                ImgCabinetOrng.IsVisible = false;
                LblCabinetOrng.IsVisible = false;
                ImgCabinetBlack.IsVisible = true;
                LblCabinetBlack.IsVisible = true;

                ImgMyRecipesOrng.IsVisible = false;
                LblMyRecipesOrng.IsVisible = false;
                ImgMyRecipesBlack.IsVisible = true;
                LblMyRecipesBlack.IsVisible = true;
            }
            else if (Helpers.Constants.IsMyRecipesPage == true)
            {
                Helpers.Constants.IsMyRecipesPage = false;
                Detail = new NavigationPage(new Pages.MyRecipes.MyRecipesPage());
                IsPresented = true;

                ImgMyRecipesOrng.IsVisible = true;
                LblMyRecipesOrng.IsVisible = true;
                ImgMyRecipesBlack.IsVisible = false;
                LblMyRecipesBlack.IsVisible = false;

                ImgProfileOrng.IsVisible = false;
                LblProfileOrng.IsVisible = false;
                ImgProfileBlack.IsVisible = true;
                LblProfileBlack.IsVisible = true;

                ImgMyCartOrng.IsVisible = false;
                LblMyCartOrng.IsVisible = false;
                ImgMyCartBlack.IsVisible = true;
                LblMyCartBlack.IsVisible = true;

                ImgCocktailOrng.IsVisible = false;
                LblCocktailOrng.IsVisible = false;
                ImgCocktailBlack.IsVisible = true;
                LblCocktailBlack.IsVisible = true;

                ImgCabinetOrng.IsVisible = false;
                LblCabinetOrng.IsVisible = false;
                ImgCabinetBlack.IsVisible = true;
                LblCabinetBlack.IsVisible = true;

                ImgPresentationOrng.IsVisible = false;
                LblPresentationOrng.IsVisible = false;
                ImgPresentationBlack.IsVisible = true;
                LblPresentationBlack.IsVisible = true;
            }

            else if (Helpers.Constants.IsRecipeDetailPage == true)
            {
                Helpers.Constants.IsRecipeDetailPage = false;
                Detail = new NavigationPage(new Pages.RecipeDetail.RecipeDetailPage(Helpers.Constants.RecipeDetail));
                IsPresented = false;
            }
            else if (Helpers.Constants.IsRecipeDetail == true)
            {
                Helpers.Constants.IsRecipeDetail = false;
                Detail = new NavigationPage(new Pages.RecipeDetail.RecipeDetailPage(Helpers.Constants.RecipeDetail));
                IsPresented = true;
            }

            else
            {
                Detail = new NavigationPage(new Pages.RecipePage.RecipePage());
                IsPresented = false;
            }

            Commands.SetTap(grdCock, new Command(() =>
            {
                Detail = new NavigationPage(new Pages.RecipePage.RecipePage());
                IsPresented = false;
            }));

            Commands.SetTap(grdCart, new Command(() =>
            {
                Detail = new NavigationPage(new Pages.CartPage.CartPage());
                IsPresented = false;
            }));

            Commands.SetTap(grdCabinet, new Command(() =>
            {
                Detail = new NavigationPage(new Pages.Cabinet.CabinetPage());
                IsPresented = false;
            }));

            Commands.SetTap(grdProfile, new Command(async () =>
            {
                Detail = new NavigationPage(new Pages.UserProfile.UserProfilePage());
                IsPresented = false;
            }));

            Commands.SetTap(grdPresentation, new Command(async () =>
            {
                Detail = new NavigationPage(new Pages.RecipePage.NewRecipePage());
                IsPresented = false;
            }));

            Commands.SetTap(grdMyRecipes, new Command(async () =>
            {
                Detail = new NavigationPage(new Pages.MyRecipes.MyRecipesPage());
                IsPresented = false;
            }));
        }

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

                lblLogin.Text = "Login";
                lblUserName.Text = "test user";
                ImgProfilePic.Source = "profile_noimage.png";

                Xamarin.Forms.MessagingCenter.Send<string>("", "AuthLogin");
            }
        }

        //private async void Cocktails_Clicked(object sender, EventArgs e)
        //{
        //    Detail = new NavigationPage(new Pages.RecipePage.RecipePage());
        //    IsPresented = false;
        //}

        //private async void MyProfile_Clicked(object sender, EventArgs e)
        //{
        //    Detail = new NavigationPage(new Pages.UserProfile.UserProfilePage());
        //    IsPresented = false;
        //}

        //private void MyCart_Clicked(object sender, EventArgs e)
        //{
        //    Detail = new NavigationPage(new Pages.CartPage.CartPage());
        //    IsPresented = false;
        //}

        //private void MyCabinets_Clicked(object sender, EventArgs e)
        //{
        //    Detail = new NavigationPage(new Pages.Cabinet.CabinetPage());
        //    IsPresented = false;
        //}

        //private void RecipedetailClicked(object sender, EventArgs e)
        //{
        //    //App.masterDetailPage.Detail = new Pages.RecipeDetail.RecipeDetailPage();
        //    App.masterDetailPage.IsPresented = false;
        //}
    }
}