using BFAPI;
using MixItUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MixItUp.Pages.RecipePage
{
    public class NewRecipePageVM : BaseViewModel
    {
        public NewRecipePageVM(INavigation _Nav)
        {
            Navigation = _Nav;
        }

        #region Properties
        private ObservableCollection<NewRecipeModel> _PermanentRecipeList;
        public ObservableCollection<NewRecipeModel> PermanentRecipeList
        {
            get { return _PermanentRecipeList; }
            set
            {
                if (_PermanentRecipeList != value)
                {
                    _PermanentRecipeList = value;
                    OnPropertyChanged("PermanentRecipeList");
                }
            }
        }

        private ObservableCollection<NewRecipeModel> _TempRecipeList;
        public ObservableCollection<NewRecipeModel> TempRecipeList
        {
            get { return _TempRecipeList; }
            set
            {
                if (_TempRecipeList != value)
                {
                    _TempRecipeList = value;
                    OnPropertyChanged("TempRecipeList");
                }
            }
        }
        public Picture DefaultImage { get; set; }

        private ObservableCollection<NewRecipeModel> _RecipeList;
        public ObservableCollection<NewRecipeModel> RecipeList
        {
            get { return _RecipeList; }
            set
            {
                if (_RecipeList != value)
                {
                    _RecipeList = value;
                    OnPropertyChanged("RecipeList");
                }
            }
        }
        #endregion

        #region Delegate Command
        #endregion

        #region Method
        #endregion

    }
}
