using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFAPI;
using ExtensionLibraryU;
using MixItUp.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using CheckBox = Plugin.InputKit.Shared.Controls.CheckBox;

namespace MixItUp.Pages.RecipeDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        protected RecipeDetailVM recipeDetailVM;
        private Recipe _recipe;
        public RecipeDetailPage(Recipe p)
        {
            InitializeComponent();
            recipeDetailVM = new RecipeDetailVM(this.Navigation, p);
            this.BindingContext = recipeDetailVM;

            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            _recipe = p;
        }

        //private void LvNews_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem == null) return;
        //    ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        //}

        private async void mastermenu(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            // Helpers.Constants.IsRecipeDetail = true;          
            // App.Current.MainPage = new Pages.MasterPage.MasterPage();
            //App.masterDetailPage.IsPresented = true;
        }



        private async void NutritionPagenavigation(object sender, EventArgs e)
        {
            // App.masterDetailPage.Detail = new Pages.IngredientPage.IngredientPage();
            await Navigation.PushModalAsync(new Pages.NutritionPage.NutritionPage(_recipe));
        }

        private async void ReviewNavigation(object sender, EventArgs e)
        {
            //App.masterDetailPage.Detail = new Pages.ReviewPage.ReviewPage();
            await Navigation.PushModalAsync(new Pages.ReviewPage.ReviewPage(_recipe));
        }

        private async void EditRecipeNaviagtion(object sender, EventArgs e)
        {
            // App.masterDetailPage.Detail = new Pages.CustomRecipe.CustomRecipepage();
            await Navigation.PushModalAsync(new Pages.CustomRecipe.CustomRecipePage(_recipe));
        }

        private void AddToCabinet_OnClicked(object sender, EventArgs e)
        {
            if (((ImageButton)sender).BindingContext is RecipeDetailModel ingredient)
            {
                var selectedIngredient = Main.Ingredients.FirstOrDefault(x => x.Id == ingredient.Id);
                if (selectedIngredient != null)
                {
                    var ci = new Cart(Main.GetConnection<Cart>()).GetAll()?.FirstOrDefault(x => x.IngredientId == selectedIngredient.Id);
                    if (ci == null)
                    {
                        ci = new Cart(Main.GetConnection<Cart>())
                        {
                            Ingredient = selectedIngredient,
                            IngredientId = selectedIngredient.Id
                        };
                        ci.Add();
                        DependencyService.Get<IMessage>().ShortAlert($"{selectedIngredient.Name} has been added to your cart.");
                    }
                    else
                    {
                        ci.Quantity++;
                        ci.Update();
                        DependencyService.Get<IMessage>().ShortAlert($"Your cart now has {ci.Quantity} bottles of {selectedIngredient.Name} in it.");
                    }
                }
            }
        }

        private bool _internal;
        private void Ingredient_OnCheckChanged(object sender, EventArgs e)
        {
            if (_internal) return;

            var chk = (CheckBox)sender;
            if (((CheckBox)sender).BindingContext is RecipeDetailModel ingredient)
            {
                var selectedIngredient = Main.Ingredients.FirstOrDefault(x => x.Id == ingredient.Id);
                if (selectedIngredient != null)
                {
                    var ci = new CabinetIngredient(Main.GetConnection<CabinetIngredient>()).GetAll(Main.SelectedCabinet.Id)?.FirstOrDefault(x => x.IngredientId == selectedIngredient.Id);
                    if (chk.IsChecked)
                    {
                        if (ci == null)
                        {
                            ci = new CabinetIngredient(Main.GetConnection<CabinetIngredient>())
                            {
                                Ingredient = selectedIngredient,
                                IngredientId = selectedIngredient.Id,
                            };
                            ci.Add(Main.SelectedCabinet.Id);
                            _internal = true;
                            ingredient.InCabinet = true;
                            _internal = false;
                            DependencyService.Get<IMessage>().ShortAlert($"{selectedIngredient.Name} has been added to the '{Main.SelectedCabinet.Name}' cabinet.");
                        }
                    }
                    else
                    {
                        if (ci != null)
                        {
                            ci.Delete();
                            _internal = true;
                            ingredient.InCabinet = false;
                            _internal = false;
                            DependencyService.Get<IMessage>().ShortAlert($"{selectedIngredient.Name} has been removed from the '{Main.SelectedCabinet.Name}' cabinet.");
                        }
                    }
                }
            }
        }
    }
}