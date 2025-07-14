using MixItUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFAPI;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.CartPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public static List<Ingredient> CartList = new List<Ingredient>();
        public CartPage()
        {
            InitializeComponent();
            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            CartList = new List<Ingredient>();
            RecipeList.ItemsSource = CartList;
        }

        private async void MasterMenu(object sender, EventArgs e)
        {
            Helpers.Constants.IsCartPage = true;
            //App.masterDetailPage.IsPresented = true;
            App.Current.MainPage = new Pages.MasterPage.MasterPage();

        }

        private void RecipeClicked(object sender, ItemTappedEventArgs e)
        {

        }

        private void RecipeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            RecipeList.SelectedItem = null;
        }


        private void IconSearch_Tapped(object sender, EventArgs e)
        {
            if (GrdSearchIcon.IsVisible == true)
            {
                txtSearchBar.IsVisible = true;
                CartLst.IsVisible = false;
            }
            else
            {
                txtSearchBar.IsVisible = false;
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                RecipeList.ItemsSource = CartList;
            else
                RecipeList.ItemsSource = CartList.Where(i => i.Name.ToLower().Contains(e.NewTextValue)).ToList();



        }
        private async void back_EventTapped(object sender, EventArgs e)
        {
            if (txtSearchBar.IsVisible == true)
            {
                txtSearchBar.IsVisible = false;
                CartLst.IsVisible = true;
            }
            else
            {
                txtSearchBar.IsVisible = true;
            }
        }

    }
}