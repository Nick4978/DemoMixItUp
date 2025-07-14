using MixItUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MixItUp.Pages.Cabinet
{
   public class CabinetPageVM : BaseViewModel
    {
        //TODO : To Define Constructor..
        public CabinetPageVM(INavigation _Nav)
        {
            Navigation = _Nav;
            IngredientList = new ObservableCollection<CabinetModel>
            {
                new CabinetModel{Types="Bourbon"},
                new CabinetModel{Types="Skotch"},
                new CabinetModel{Types="Vodka"},
                new CabinetModel{Types="Whisky"},
                new CabinetModel{Types="Gin"},
                new CabinetModel{Types="Baijju"},
                new CabinetModel{Types="Taquila"},
                new CabinetModel{Types="Rum"},
                new CabinetModel{Types="Brandy"},
                new CabinetModel{Types="Singnai"},
                new CabinetModel{Types="Soju"},
            };
        }

        #region properties
        private ObservableCollection<CabinetModel> _IngredientList;
        public ObservableCollection<CabinetModel> IngredientList
        {
            get { return _IngredientList; }
            set
            {
                if (_IngredientList != value)
                {
                    _IngredientList = value;
                    OnPropertyChanged("IngredientList");
                }
            }
        }
        #endregion
    }
}
