using BFAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MixItUp.Pages.MyRecipes
{
    public class MyRecipesVM : BaseViewModel
    {

        //TODO : To Define Constructor..
        public MyRecipesVM(INavigation _Nav)
        {
            Navigation = _Nav;
           
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

        #endregion
    }
}
