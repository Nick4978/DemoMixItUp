using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFAPI;
using MixItUp.Helpers;
using MixItUp.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.CustomRecipe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomRecipePage : ContentPage
    {
        // Database Initialize
        Database MixItUpCategoryDb;
        protected CustomRecipepageVM customRecipepageVM;
        private Recipe _recipe;

        public CustomRecipePage(Recipe r)
        {
            InitializeComponent();

            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            customRecipepageVM = new CustomRecipepageVM(this.Navigation, r);
            this.BindingContext = customRecipepageVM;

            //CategoriesName.ItemsSource = Constants.lstCategories;
            lvCategoriesNamehList.ItemsSource = Constants.lstCategories;
            customRecipepageVM.Recipe = r;
        }

        //public CustomRecipePage (Recipe r) : this()
        //{

        //    customRecipepageVM.Recipe = r;
        //}


        //TODO : To Define Listview Item Selected Event...
        private void LvNews_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

        private async void mastermenu(object sender, EventArgs e) 
        {
            await Navigation.PopModalAsync();
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
                Row0_Height.Height =  GridLength.Auto;
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