using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BFAPI;
using ExtensionLibraryU;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MixItUp
{
    public partial class App : Application, INotifyPropertyChanged
    {
        public static MasterDetailPage masterDetailPage = new MasterDetailPage();
        public static bool isGoogleAuth;
        public static bool VarGmail = false;
        public App(string dbPath = "")
        {
            try
            {
                InitializeComponent();

                Main.DbPath = dbPath;

                Main.Categories = new Category(Main.GetConnection<Category>()).GetAll().OrderBy(y => y.OrderNo).ToList();
                Main.Categories.Add(new Category() { Name = "A-Z", OrderNo = -4 });
                Main.Categories.Add(new Category() { Name = "Favorites", OrderNo = -3 });
                Main.Categories.Add(new Category() { Name = "My Cabinet", OrderNo = -2 });
                Main.Categories.Add(new Category() { Name = "My Recipes", OrderNo = -1 });

                Main.Cabinets = new Cabinet(Main.GetConnection<Cabinet>()).GetAll();
                Main.Ingredients = new Ingredient(Main.GetConnection<Ingredient>()).GetAll().Where(x => x.Type != Ingredient.Types.Garnish).ToList();

                Main.SelectedCabinet = Main.Cabinets.FirstOrDefault();
                Main.CabinetIngredients = new CabinetIngredient(Main.GetConnection<CabinetIngredient>()).GetAll(Main.SelectedCabinet?.Id).ToObservableCollection();


                //masterDetailPage.Master = new Pages.ProfilePage.ProfilePage();
                //masterDetailPage.Detail = new Pages.RecipePage.RecipePage();
                //MainPage = masterDetailPage;
                //  MainPage = new Pages.MasterPage.MasterPage();

                MainPage = new Pages.AnimateSplash.AnimateSplash();
                // MainPage = new Pages.RecipePage.NewTestPage();

                // MainPage = new Pages.RecipePage.NewTestPage();

                //MainPage = new Pages.RecipeDetail.RecipeDetailPage();
                //var MasterPage = new MasterDetailPage(); 
                //MasterPage.Master = new Pages.ProfilePage.ProfilePage();
                //MasterPage.Detail = new Pages.RecipePage.RecipePage();
                //MainPage = MasterPage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //For Google Auth ...
        public async static Task NavigationToProfile()
        {
            isGoogleAuth = true;
            Xamarin.Forms.MessagingCenter.Send<string>(Convert.ToString(Helpers.Constants.isAuthLogin), "AuthLogin");
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
