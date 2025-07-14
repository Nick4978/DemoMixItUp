using MixItUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFAPI;
using Xamarin.Forms;

namespace MixItUp.Pages.RecipePage
{
    public class RecipePageVM : BaseViewModel
    {

        public RecipePageVM(INavigation _Nav)
        {
            Navigation = _Nav;

            if (Helpers.Settings.SubscriptionSettings == "True")
            {
                AdsIsVisible = false;
            }
            else
            {
                AdsIsVisible = true;
            }
        }

        private bool _AdsIsVisible;
        public bool AdsIsVisible
        {
            get { return _AdsIsVisible; }
            set
            {
                if (_AdsIsVisible != value)
                {
                    _AdsIsVisible = value;
                    OnPropertyChanged("AdsIsVisible");
                }
            }
        }

        private ObservableCollection<RecipeCarouselItem> _originalRecipeCarousel = new ObservableCollection<RecipeCarouselItem>();
        public ObservableCollection<RecipeCarouselItem> OriginalRecipeCarousel
        {
            get => _originalRecipeCarousel;
            set
            {
                _originalRecipeCarousel = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RecipeCarouselItem> _recipeCarousel = new ObservableCollection<RecipeCarouselItem>();
        public ObservableCollection<RecipeCarouselItem> RecipeCarousel
        {
            get => _recipeCarousel;
            set
            {
                _recipeCarousel = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ProductCategory> _categories = new ObservableCollection<ProductCategory>();
        public ObservableCollection<ProductCategory> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public Picture DefaultImage { get; set; }

        private ObservableCollection<Alphabet> _alphabetList = new ObservableCollection<Alphabet>();
        public ObservableCollection<Alphabet> AlphabetList
        {
            get => _alphabetList;
            set
            {
                _alphabetList = value;
                OnPropertyChanged();
            }
        }
    }
}


