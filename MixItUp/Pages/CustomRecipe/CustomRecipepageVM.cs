using MixItUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BFAPI;
using Xamarin.Forms;

namespace MixItUp.Pages.CustomRecipe
{
    public class CustomRecipepageVM : BaseViewModel
    {
        //TODO : To Define Constructor..
        public CustomRecipepageVM(INavigation _Nav, Recipe recipe)
        {
            Navigation = _Nav;

            foreach (var i in new Ingredient(Main.GetConnection<Ingredient>()).GetAll())
            {
                IngredientList.Add(new CustomRecipeModel() { Id = i.Id, Ingerdient = i.Name });
            }

            DefaultImage = recipe.Images[0];
            CategoriesName = "Select";
        }
        #region properties

        public Picture DefaultImage { get; set; }

        private Recipe _recipe;
        public Recipe Recipe
        {
            get { return _recipe; }
            set
            {
                if (_recipe != value)
                {
                    _recipe = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<CustomRecipeModel> _ingredientList = new ObservableCollection<CustomRecipeModel>();
        public ObservableCollection<CustomRecipeModel> IngredientList
        {
            get { return _ingredientList; }
            set
            {
                if (_ingredientList != value)
                {
                    _ingredientList = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _CategoriesName ;
        public string CategoriesName
        {
            get { return _CategoriesName; }
            set
            {
                if (_CategoriesName != value)
                {
                    _CategoriesName = value;
                    OnPropertyChanged("CategoriesName");
                }
            }
        }

        

        

        #endregion
    }
}
