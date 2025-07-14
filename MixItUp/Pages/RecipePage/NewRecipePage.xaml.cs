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
using System.Timers;
using Xamarin.Forms;
using MixItUp.CustomControls;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.RecipePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRecipePage : ContentPage
    {
        readonly RecipePageVM RecipeVM;
        protected NewRecipePageVM newRecipePageVM;
        private System.Timers.Timer _tmr = new Timer(250);
        // List<T> GetList = null;
        private int pageCount = 1;
        private int pageLength = Convert.ToInt32(App.Current.MainPage.Width);

        public NewRecipePage()
        {
            InitializeComponent();
            RecipeVM = new RecipePageVM(this.Navigation);
            BindingContext = RecipeVM;

            newRecipePageVM = new NewRecipePageVM(this.Navigation);
            this.BindingContext = newRecipePageVM;

            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private void TabScroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (TabScroll.ScrollX > pageLength)
            {
                pageCount = pageCount + 1;
                // pageLength = Convert.ToInt32(TabScroll.ScrollX);
                pageLength = pageLength + Convert.ToInt32(App.Current.MainPage.Width);
                GetAllRecipe(pageCount);
            }
        }

        public void GetAllRecipe(int pagenum)
        {
            var lastpage = pagenum - 1;
            int skipcount = lastpage * 15;
            List<NewRecipeModel> recipe = new System.Collections.ObjectModel.ObservableCollection<NewRecipeModel>(newRecipePageVM.PermanentRecipeList.Skip(skipcount).Take<NewRecipeModel>(15)).ToList();
            List<NewRecipeModel> newRecipes = newRecipePageVM.TempRecipeList.ToList();
            newRecipes.AddRange(recipe);
            newRecipePageVM.TempRecipeList = newRecipes.ToObservableCollection();
            lstSyrup.ItemsSource = newRecipePageVM.TempRecipeList;
        }

        protected async override void OnAppearing()
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    base.OnAppearing();
                    var carousel = new ObservableCollection<RecipeCarouselItem>();
                    var carouselOriginal = new ObservableCollection<RecipeCarouselItem>();
                    var categories = new ObservableCollection<ProductCategory>();

                    foreach (var c in Main.Categories.OrderBy(y => y.OrderNo))
                    {
                        categories.Add(new ProductCategory()
                        {
                            Id = c.Id,
                            Name = c.Name,
                            TextDecorationStyle = TextDecorations.None
                        });

                        if (c.Name == "A-Z")
                        {
                            carousel.Add(new RecipeCarouselItem()
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                Recipes = Main.MasterRecipeList.ToObservableCollection()
                            });
                            carouselOriginal.Add(new RecipeCarouselItem()
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                Recipes = Main.MasterRecipeList.ToObservableCollection()
                            });
                        }
                        else if (c.Name == "Favorites")
                        {
                            carousel.Add(new RecipeCarouselItem()
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                Recipes = Main.MasterRecipeList.Where(x => x.IsFavorite).ToObservableCollection()
                            });
                            carouselOriginal.Add(new RecipeCarouselItem()
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                Recipes = Main.MasterRecipeList.Where(x => x.IsFavorite).ToObservableCollection()
                            });
                        }
                        else if (c.Name == "My Cabinet")
                        {
                            var results = new ObservableCollection<Recipe>();
                            foreach (var rcp in Main.MasterRecipeList)
                            {
                                var isPossible = true;
                                foreach (var i in rcp.IngredientIds.Split(',').ToList())
                                {
                                    if (Main.CabinetIngredients?.FirstOrDefault(x => x.Id == i) == null)
                                    {
                                        isPossible = false;
                                        break;
                                    }
                                }
                                if (isPossible) results.Add(rcp);
                            }

                            carousel.Add(new RecipeCarouselItem()
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                Recipes = results
                            });
                            carouselOriginal.Add(new RecipeCarouselItem()
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                Recipes = results
                            });
                        }
                        else
                        {
                            carousel.Add(new RecipeCarouselItem()
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                Recipes = Main.MasterRecipeList.Where(x => x.CategoryIds.Contains(c.Id))
                                .ToObservableCollection()
                            });
                            carouselOriginal.Add(new RecipeCarouselItem()
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                Recipes = Main.MasterRecipeList.Where(x => x.CategoryIds.Contains(c.Id))
                                .ToObservableCollection()
                            });
                        }
                    }
                    var GetList = carousel[0].Recipes;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var RecipeList = new ObservableCollection<NewRecipeModel>();
                        foreach (var item in GetList)
                        {
                            RecipeList.Add(new NewRecipeModel
                            {
                                Name = item.Name,
                                DefaultImage = item.Images[0],
                                Description = item.Description,
                                Ingredients = item.Ingredients.ToObservableCollection(),
                                Id = item.Id
                            });
                        }

                        
                        Helpers.Constants.permanentRecipe = new ObservableCollection<NewRecipeModel>(RecipeList);
                        newRecipePageVM.PermanentRecipeList = Helpers.Constants.permanentRecipe;
                        newRecipePageVM.TempRecipeList = new System.Collections.ObjectModel.ObservableCollection<NewRecipeModel>(newRecipePageVM.PermanentRecipeList.Take<NewRecipeModel>(15));

                        RecipeTitle.Text = newRecipePageVM.TempRecipeList[0].Name;
                        DirectionsText.Text = newRecipePageVM.TempRecipeList[0].Description;
                        newRecipePageVM.DefaultImage = newRecipePageVM.TempRecipeList[0].DefaultImage;

                        int count = 1;

                        foreach (var item in newRecipePageVM.TempRecipeList)
                        {
                            if (count == 1)
                            {
                                item.TextColors = "#ff4500";
                            }
                            count++;
                        }
                    });
                });
            }
            catch (Exception Ex)
            {
            }
        }

        private void MasterMenu(object sender, EventArgs e)
        {
            Helpers.Constants.IsNewRecipePage = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                App.Current.MainPage = new Pages.MasterPage.MasterPage();
                //App.masterDetailPage.IsPresented = true;
            });
        }


        /// <summary>
        /// To define the recepe item tapped event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RecipeItem_TappedEvent(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //  await Navigation.PushAsync(new Pages.RecipePage.NewTestPage());
                var item = (sender as Grid)?.BindingContext as NewRecipeModel;
                if (item == null) return;

                RecipeTitle.Text = item.Name;
                DirectionsText.Text = item.Description;
                newRecipePageVM.DefaultImage = item.Images;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    foreach (var items in newRecipePageVM.TempRecipeList)
                    {
                        if (item.Id == items.Id)
                        {
                            items.TextColors = "#ff4500";
                        }
                        else
                        {
                            items.TextColors = "#000000";
                        }
                    }
                });
            });
        }

        private void IconSearch_Tapped(object sender, EventArgs e)
        {
            if (GrdSearchIcon.IsVisible)
            {
                txtSearchBar.IsVisible = true;
                RecipeLst.IsVisible = false;
                _tmr.Start();
                /*
                                Task.Delay(500);
                                SearchBox.Focus();
                                SearchBox.Focus();
                                Task.Delay(500);
                                SearchBox.CursorPosition = 1;               
                */
            }
            else
            {
                txtSearchBar.IsVisible = false;
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //  var recipes = RecipeVM.OriginalRecipeCarousel[RecipeCarousel.Position].Recipes;

                string filter = SearchBox.Text;
                lstSyrup.ItemsSource = newRecipePageVM.TempRecipeList.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
                //lstSyrup.EndRefresh();


                //var recipes = RecipeVM.OriginalRecipeCarousel[RecipeCarousel.Position].Recipes;
                //if (string.IsNullOrWhiteSpace(e.NewTextValue))
                //{
                //    _currentPage.Recipes = recipes;
                //    ResetAlphabetList();
                //}
                //else
                //{
                //    _currentPage.Recipes = recipes.Where(x => x.Name.ToLower().Contains(e.NewTextValue.ToLower())).ToObservableCollection();
                //    ResetAlphabetList();
                //}
            }
            catch (Exception ex)
            {
            }
        }

        private RecipeCarouselItem _currentPage;
        private void RecipeCarousel_OnCurrentItemChanged(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            if (RecipeVM.RecipeCarousel == null) return;

            if (_currentPage != null || RecipeVM.RecipeCarousel[e.NewValue] == _currentPage) ;
            _currentPage = RecipeVM.RecipeCarousel[e.NewValue];

            RecipeVM.Categories = RecipeVM.Categories.Select(c =>
            {
                c.TextDecorationStyle = TextDecorations.None;
                return c;
            }).ToObservableCollection();

            var selectedCategory = RecipeVM.Categories.FirstOrDefault(x => x.Name == _currentPage.Name);
            //if (selectedCategory != null)
            //{
            //    selectedCategory.TextDecorationStyle = TextDecorations.Underline;
            //    _horizontalListViewSelectedItem = selectedCategory.Name;
            //    SelectedCategory = selectedCategory.Name;

            //    ResetAlphabetList();

            //    var elem = this.FindChildren<Grid>(TapableList).FirstOrDefault(x => x.ClassId == selectedCategory.Name);
            //    var pos = TabScroll.GetScrollPositionForElement(elem, ScrollToPosition.Start);
            //    TabScroll.ScrollToAsync(pos.X - 50, pos.Y, true);
            //}
        }

        private void ResetAlphabetList()
        {
            if (_currentPage == null) return;

            var alphabetList = new ObservableCollection<Alphabet>();
            for (var i = 48; i <= 57; i++)
            {
                var exists = _currentPage.Recipes?.FirstOrDefault(x => x.Name.StartsWith(((char)i).ToString())) != null;
                if (exists)
                {
                    alphabetList.Add(new Alphabet() { AlphabetName = "#" });
                    break;
                }
            }
            for (var i = 65; i <= 90; i++)
            {
                var exists = _currentPage.Recipes?.FirstOrDefault(x => x.Name.StartsWith(((char)i).ToString())) != null;
                if (exists) alphabetList.Add(new Alphabet() { AlphabetName = ((char)i).ToString() });
            }
            RecipeVM.AlphabetList = alphabetList;
        }

        private void back_EventTapped(object sender, EventArgs e)
        {
            if (txtSearchBar.IsVisible)
            {
                SearchBox.Text = "";
                SearchBox.Unfocus();
                txtSearchBar.IsVisible = false;
                RecipeLst.IsVisible = true;
            }
            else
            {
                txtSearchBar.IsVisible = true;
            }
        }

        /// <summary>
        /// TODO:To define the back button tapped event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackListBtn_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (TabScroll.ScrollX > 200)
                    TabScroll.ScrollToAsync(TabScroll.ScrollX - 200, 0, true);
                else
                    TabScroll.ScrollToAsync(0, 0, true);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        ///  TODO:To define the Forward button tapped event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForwardListBtn_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (TabScroll.ScrollX < TabScroll.ContentSize.Width - TabScroll.Width - 200)
                    TabScroll.ScrollToAsync(TabScroll.ScrollX + 200, 0, true);
                else
                    TabScroll.ScrollToAsync(TabScroll.ContentSize.Width - TabScroll.Width, 0, true);

            }
            catch (Exception ex)
            {
            }
        }
    }
}