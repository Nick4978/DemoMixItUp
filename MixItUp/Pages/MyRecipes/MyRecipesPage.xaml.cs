using BFAPI;
using MixItUp.Helpers;
using MixItUp.Pages.CustomRecipe;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.MyRecipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRecipesPage : ContentPage
    {
        // Database Initialize
        Database MixItUpCategoryDb;
        protected MyRecipesVM myRecipesVM;
        private Recipe _recipe;

        public MyRecipesPage()
        {
            InitializeComponent();

            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            myRecipesVM = new MyRecipesVM(this.Navigation);
            this.BindingContext = myRecipesVM;
            
            lvMyCategoriesNameh.ItemsSource = Constants.lstCategories;              
        }

        private void IconSearch_Tapped(object sender, EventArgs e)
        {
            if (GrdSearchIcon.IsVisible)
            {
                txtSearchBar.IsVisible = true;
                MyRecipesLst.IsVisible = false;
            }
            else
            {
                txtSearchBar.IsVisible = false;
            }
        }

        private void back_EventTapped(object sender, EventArgs e)
        {
            if (txtSearchBar.IsVisible == true)
            {
                txtSearchBar.IsVisible = false;
                MyRecipesLst.IsVisible = true;
            }
            else
            {
                txtSearchBar.IsVisible = true;
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(e.NewTextValue))
            //    RecipeList.ItemsSource = CartList;
            //else
            //    RecipeList.ItemsSource = CartList.Where(i => i.Name.ToLower().Contains(e.NewTextValue)).ToList();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Helpers.Constants.IsRecipePage = true;
            App.Current.MainPage = new Pages.MasterPage.MasterPage();
            //App.masterDetailPage.IsPresented = true;
        }


        //TODO : To Define Listview Item Selected Event...
        private void LvNews_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }


        private void MasterMenu_Tapped(object sender, EventArgs e)
        {
            Helpers.Constants.IsMyRecipesPage = true;
            App.Current.MainPage = new Pages.MasterPage.MasterPage();
        }


        private void SelectCategoryMulti_Focused(object sender, EventArgs e)
        {
            if (PopupCategoriesName.IsVisible == false)
            {
                PopupCategoriesName.IsVisible = true;
            }
            else
            {
                PopupCategoriesName.IsVisible = false;
            }
        }

        private void MultiSelectCategories_Tapped(object sender, EventArgs e)
        {
            var Item = (sender as Label);
            var SelectItem = Item.Text;
            if (SelectItem != null && !SelectCategoryMulti.Text.Contains(SelectItem))
            {
                SelectCategoryMulti.Text += !string.IsNullOrEmpty(SelectCategoryMulti.Text) ? "," + SelectItem : SelectItem;
                Row0_Height.Height = GridLength.Auto;
            }
        }

        private void Save_Tapped(object sender, EventArgs e)
        {
            //LocalDatabaseModel.CategoryDataTable Categoryitem = new LocalDatabaseModel.CategoryDataTable();
            //Categoryitem.RecipeType = customRecipepageVM.CategoriesName;

            //MixItUpCategoryDb.AddCategoryData(Categoryitem);
        }

       
    }
}