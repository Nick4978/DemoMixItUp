using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BFAPI;
using MixItUp.Pages;

namespace MixItUp.Model
{
    public class RecipeCarouselItem : BaseViewModel
    {
        private string _categoryId;
        public string CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Recipe> _recipes = new ObservableCollection<Recipe>();
        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes;
            set
            {
                _recipes = value;
                OnPropertyChanged();
            }
        }
    }
}
