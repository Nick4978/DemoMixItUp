using Acr.UserDialogs;
using BFAPI;
using ExtensionLibraryU;
using MixItUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.RecipePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTestPage : ContentPage
    {
        protected NewTestPageVM newTestPageVM; 
        private int pageCount = 1; 
        public NewTestPage()
        {
            InitializeComponent();
            newTestPageVM = new NewTestPageVM(this.Navigation);
            this.BindingContext = newTestPageVM;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(async () =>
            {
                newTestPageVM.PermanentRecipeList = Helpers.Constants.permanentRecipeLists;
                // LstRecipe.ItemsSource = Helpers.Constants.permanentRecipeLists;

                newTestPageVM.TempRecipeList = new System.Collections.ObjectModel.ObservableCollection<NewRecipeModel>(newTestPageVM.PermanentRecipeList.Take<NewRecipeModel>(10));

            });
        }

        private void LstRecipe_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        { 
            var currentIdx = newTestPageVM.TempRecipeList.IndexOf((NewRecipeModel)e.Item); 

            if (currentIdx == newTestPageVM.TempRecipeList.Count - 1)
            {
                pageCount = pageCount + 1;
                GetAllRecipe(pageCount);
            }
        }

        public void GetAllRecipe(int pagenum)
        {
            var lastpage = pagenum - 1;
            int skipcount = lastpage * 10;

            List<NewRecipeModel> recipe = new System.Collections.ObjectModel.ObservableCollection<NewRecipeModel>(newTestPageVM.PermanentRecipeList.Skip(skipcount).Take<NewRecipeModel>(10)).ToList();
            List<NewRecipeModel> newRecipes = newTestPageVM.TempRecipeList.ToList();
            newRecipes.AddRange(recipe);
            newTestPageVM.TempRecipeList = newRecipes.ToObservableCollection();
            //LstRecipe.ItemsSource = newTestPageVM.TempRecipeList;
        }
    }
}