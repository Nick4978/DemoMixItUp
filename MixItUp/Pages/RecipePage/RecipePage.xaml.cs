using MixItUp.Model;
using MLToolkit.Forms.SwipeCardView.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using BFAPI;
using ExtensionLibraryU;
using MixItUp.CustomControls;
using SuaveControls.DynamicStackLayout;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ListView = Xamarin.Forms.ListView;
using MixItUp.Helpers;

namespace MixItUp.Pages.RecipePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePage : ContentPage
    {
        public event EventHandler<MasterRecipeChangedEventArgs> OnMasterRecipeListChanged = delegate { };

        public void MasterRecipeListChanged(object sender, Recipe recipe)
        {
            OnMasterRecipeListChanged?.Invoke(sender, new MasterRecipeChangedEventArgs(recipe));
        }

        public void MasterRecipeListChanged(object sender, ObservableCollection<Recipe> recipes)
        {
            OnMasterRecipeListChanged?.Invoke(sender, new MasterRecipeChangedEventArgs(recipes));
        }


        private double _screenWidth;
        public double ScreenWidth
        {
            get { return _screenWidth; }
            set
            {
                if (_screenWidth != value)
                {
                    _screenWidth = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _scrollToItem = "";
        public string ScrollToItem
        {
            get { return _scrollToItem; }
            set
            {
                if (_scrollToItem != value)
                {
                    _scrollToItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private Recipe _scrollToRecipe;
        public Recipe ScrollToRecipe
        {
            get { return _scrollToRecipe; }
            set
            {
                if (_scrollToRecipe != value)
                {
                    _scrollToRecipe = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedCategory = "";
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        private System.Timers.Timer _tmr = new Timer(250);
        private Recipe _lastItem;
        readonly RecipePageVM RecipeVM;
        private string _horizontalListViewSelectedItem = "Favorites";

        private double width;
        private double height;
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;

                ScreenWidth = width;

                var oldItems = RecipeVM.RecipeCarousel;
                RecipeVM.RecipeCarousel = null;
                RecipeVM.RecipeCarousel = oldItems;

                if (_lastItem != null) ScrollToRecipe = _lastItem;
            }
        }


        public RecipePage()
        {
            InitializeComponent();

            OnMasterRecipeListChanged += MainOnOnMasterRecipeListChanged;

            var recipes = new Recipe(Main.GetConnection<Recipe>());
            Task.Run(() =>
            {
                try
                {
                    var newRecipes = recipes.GetAll().ToObservableCollection();
                    MasterRecipeListChanged(this, newRecipes);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            _tmr.Elapsed += TmrOnElapsed;

            RecipeVM = new RecipePageVM(this.Navigation);

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

            BindingContext = RecipeVM;

            var selectedCategory = RecipeVM.Categories.FirstOrDefault(x => x.Name == _horizontalListViewSelectedItem);
            if (selectedCategory != null)
            {
                selectedCategory.TextDecorationStyle = TextDecorations.Underline;
                _horizontalListViewSelectedItem = selectedCategory.Name;
            }

            RecipeVM.Categories = categories;
            RecipeVM.OriginalRecipeCarousel = carouselOriginal;
            RecipeVM.RecipeCarousel = carousel;

            foreach (var item in categories)
            {
                if (item.Name !=null)
                {
                    Constants.lstCategories.Add(item.Name);
                }
            }
        }

        private void TmrOnElapsed(object sender, ElapsedEventArgs e)
        {
            _tmr.Stop();
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                SearchBox.Focus();
                SearchBox.CursorPosition = 1;
            });
        }

        private string _lastLetter = "";
        private void MainOnOnMasterRecipeListChanged(object sender, MasterRecipeChangedEventArgs e)
        {
            if (e.Recipes?.Count > 0)
            {
                Main.MasterRecipeList.Clear();
                foreach (var or in RecipeVM.OriginalRecipeCarousel)
                {
                    or.Recipes.Clear();
                }
                foreach (var or in RecipeVM.RecipeCarousel)
                {
                    or.Recipes.Clear();
                }

                foreach (var r in e.Recipes)
                {
                    Main.MasterRecipeList.Add(r);

                    RecipeVM.OriginalRecipeCarousel.FirstOrDefault(x => x.Name == "A-Z")?.Recipes.Add(r);
                    RecipeVM.RecipeCarousel.FirstOrDefault(x => x.Name == "A-Z")?.Recipes.Add(r);

                    if (r.IsFavorite)
                    {
                        RecipeVM.RecipeCarousel.FirstOrDefault(x => x.Name == "Favorites")?.Recipes.Add(r);
                        RecipeVM.OriginalRecipeCarousel.FirstOrDefault(x => x.Name == "Favorites")?.Recipes.Add(r);
                    }

                    /*
                                        var isPossible = true;
                                        foreach (var i in r.GetRecipeIngredients().Where(x => x.Type != Ingredient.Types.Garnish).ToList())
                                        {
                                            if (Main.CabinetIngredients?.FirstOrDefault(x => x.IngredientId == i.IngredientId) == null)
                                            {
                                                isPossible = false;
                                                break;
                                            }
                                        }

                                        if (isPossible)
                                        {
                                            RecipeVM.RecipeCarousel.FirstOrDefault(x => x.Name == "My Cabinet")?.Recipes.Add(r);
                                            RecipeVM.OriginalRecipeCarousel.FirstOrDefault(x => x.Name == "My Cabinet")?.Recipes.Add(r);
                                        }
                    */

                    foreach (var c in r.CategoryIds.Split(',').ToList())
                    {
                        RecipeVM.OriginalRecipeCarousel.FirstOrDefault(x => x.CategoryId == c)?.Recipes.Add(r);
                        RecipeVM.RecipeCarousel.FirstOrDefault(x => x.CategoryId == c)?.Recipes.Add(r);
                    }
                }
                Dispatcher.BeginInvokeOnMainThread(ResetAlphabetList);
            }
            else
            {
                var r = e.Recipe;
                var addToPage = false;
                Main.MasterRecipeList.Add(r);

                RecipeVM.OriginalRecipeCarousel.FirstOrDefault(x => x.Name == "A-Z")?.Recipes.Add(r);
                RecipeVM.RecipeCarousel.FirstOrDefault(x => x.Name == "A-Z")?.Recipes.Add(r);
                addToPage = _currentPage?.Name == "A-Z";

                if (r.IsFavorite)
                {
                    RecipeVM.RecipeCarousel.FirstOrDefault(x => x.Name == "Favorites")?.Recipes.Add(r);
                    RecipeVM.OriginalRecipeCarousel.FirstOrDefault(x => x.Name == "Favorites")?.Recipes.Add(r);
                    addToPage = _currentPage?.Name == "Favorites";
                }

                foreach (var c in r.CategoryIds.Split(',').ToList())
                {
                    RecipeVM.OriginalRecipeCarousel.FirstOrDefault(x => x.CategoryId == c)?.Recipes.Add(r);
                    RecipeVM.RecipeCarousel.FirstOrDefault(x => x.CategoryId == c)?.Recipes.Add(r);
                    addToPage = _currentPage?.CategoryId == c;
                }

                var isNumber = int.TryParse(r.Name.Substring(0, 1), out var num);
                var curLetter = isNumber ? "#" : r.Name.Substring(0, 1).ToUpper();
                if (curLetter != _lastLetter)
                {
                    if (_currentPage != null)
                    {
                        if (addToPage) RecipeVM.AlphabetList.Add(new Alphabet() { AlphabetName = curLetter });
                    }
                    //Dispatcher.BeginInvokeOnMainThread(ResetAlphabetList);
                }
                _lastLetter = curLetter;

            }
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
                    //alphabetList.Add(new Alphabet() { AlphabetName = ((char)i).ToString() });
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

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Helpers.Constants.IsRecipePage = true;
            App.Current.MainPage = new Pages.MasterPage.MasterPage();
            //App.masterDetailPage.IsPresented = true;
        }

        private void HeartImageClicked(object sender, EventArgs e)
        {
            var selectedItem = (sender as Image)?.BindingContext as Recipe;
            if (selectedItem == null) return;

            selectedItem.IsFavorite = !selectedItem.IsFavorite;
            selectedItem.Save();

            var favoriteRecipesOriginal = RecipeVM.OriginalRecipeCarousel.FirstOrDefault(x => x.Name == "Favorites");
            var favoriteRecipes = RecipeVM.RecipeCarousel.FirstOrDefault(x => x.Name == "Favorites");


            if (selectedItem.IsFavorite)
            {
                favoriteRecipesOriginal?.Recipes.Add(selectedItem);
                favoriteRecipes?.Recipes.Add(selectedItem);
            }
            else
            {
                favoriteRecipesOriginal?.Recipes.Remove(selectedItem);
                favoriteRecipes?.Recipes.Remove(selectedItem);
            }
            if (_selectedCategory == "Favorites") ResetAlphabetList();
        }

        // Click event for Navigation of RecipeDetailPage... 
        private async void RecipeDetailPage_Tapped(object sender, EventArgs e)
        {
            var item = (sender as Grid)?.BindingContext as Recipe;
            if (item == null) return;

            var r = new Recipe(Main.GetConnection<Recipe>()).Get(item.Id);
            Helpers.Constants.RecipeDetail = r;
            Helpers.Constants.IsRecipeDetailPage = true;

            await Navigation.PushModalAsync(new RecipeDetail.RecipeDetailPage(r));
        }

        private async void RecipeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is Recipe)) return;

            var r = new Recipe(Main.GetConnection<Recipe>()).Get(((Recipe)e.SelectedItem).Id);
            ((NativeListView)sender).SelectedItem = r;
            Helpers.Constants.RecipeDetail = r;
            Helpers.Constants.IsRecipeDetailPage = true;

            await Navigation.PushModalAsync(new RecipeDetail.RecipeDetailPage(r));

            //App.Current.MainPage = new Pages.MasterPage.MasterPage();
            //App.masterDetailPage.Detail = new Pages.RecipeDetail.RecipeDetailPage((Product)e.SelectedItem);
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
                var recipes = RecipeVM.OriginalRecipeCarousel[RecipeCarousel.Position].Recipes;
                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    _currentPage.Recipes = recipes;
                    ResetAlphabetList();
                }
                else
                {
                    _currentPage.Recipes = recipes.Where(x => x.Name.ToLower().Contains(e.NewTextValue.ToLower())).ToObservableCollection();
                    ResetAlphabetList();
                }
            }
            catch (Exception ex)
            {


            }
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

        private void Alphabet_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var selectedItem = (sender as Button)?.BindingContext as Alphabet;
            ScrollToItem = selectedItem?.AlphabetName;
            if (selectedItem != null) _lastItem = _currentPage.Recipes.FirstOrDefault(x => x.Name.StartsWith(selectedItem.AlphabetName));
        }

        private RecipeCarouselItem _currentPage;
        private void RecipeCarousel_OnCurrentItemChanged(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            if (RecipeVM.RecipeCarousel == null) return;

            //  if (_currentPage != null || RecipeVM.RecipeCarousel[e.NewValue] == _currentPage) return;
            if (_currentPage != null || RecipeVM.RecipeCarousel[e.NewValue] == _currentPage) ;
            _currentPage = RecipeVM.RecipeCarousel[e.NewValue];

            RecipeVM.Categories = RecipeVM.Categories.Select(c =>
            {
                c.TextDecorationStyle = TextDecorations.None;
                return c;
            }).ToObservableCollection();

            var selectedCategory = RecipeVM.Categories.FirstOrDefault(x => x.Name == _currentPage.Name);
            if (selectedCategory != null)
            {
                selectedCategory.TextDecorationStyle = TextDecorations.Underline;
                _horizontalListViewSelectedItem = selectedCategory.Name;
                SelectedCategory = selectedCategory.Name;

                ResetAlphabetList();

                var elem = this.FindChildren<Grid>(TapableList).FirstOrDefault(x => x.ClassId == selectedCategory.Name);
                var pos = TabScroll.GetScrollPositionForElement(elem, ScrollToPosition.Start);
                TabScroll.ScrollToAsync(pos.X - 50, pos.Y, true);
            }
        }

        //When User Click On Horizontal Listview
        private void HorizontalListView_OnTapped(object sender, EventArgs e)
        {
            var selectedItem = (sender as Grid)?.BindingContext as ProductCategory;

            for (var i = 0; i < RecipeVM.Categories.Count; i++)
            {
                if (RecipeVM.Categories[i].Name == selectedItem.Name)
                {
                    _currentPage = RecipeVM.RecipeCarousel[i];
                    RecipeCarousel.Position = i;
                    break;
                }
            }

            RecipeVM.Categories = RecipeVM.Categories.Select(c =>
            {
                c.TextDecorationStyle = TextDecorations.None;
                return c;
            }).ToObservableCollection();

            var selectedCategory = RecipeVM.Categories.FirstOrDefault(x => x.Name == selectedItem.Name);
            if (selectedCategory != null)
            {
                selectedCategory.TextDecorationStyle = TextDecorations.Underline;
                _horizontalListViewSelectedItem = selectedCategory.Name;
                SelectedCategory = selectedCategory.Name;

                ResetAlphabetList();

                var elem = this.FindChildren<Grid>(TapableList).FirstOrDefault(x => x.ClassId == selectedCategory.Name);
                var pos = TabScroll.GetScrollPositionForElement(elem, ScrollToPosition.Start);
                TabScroll.ScrollToAsync(pos.X - 50, pos.Y, false);
            }
        }


        private void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            _lastItem = ((Recipe)e.Item);
        }
    }
}