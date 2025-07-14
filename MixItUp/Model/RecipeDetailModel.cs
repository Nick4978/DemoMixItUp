using System;
using System.Collections.Generic;
using System.Text;
using MixItUp.Pages;

namespace MixItUp.Model
{
    public class RecipeDetailModel : BaseViewModel
    {
        public string Id { get; set; }
        public string Quantity { get; set; }
        public string Ingredient { get; set; }

        private bool _inCabinet;
        public bool InCabinet
        {
            get => _inCabinet;
            set
            {
                _inCabinet = value;
                OnPropertyChanged();
            }
        }
    }
}
