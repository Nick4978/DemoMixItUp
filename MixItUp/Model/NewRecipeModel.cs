using BFAPI;
using BFAPI.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MixItUp.Model
{
    public class NewRecipeModel : BindableObject
    {
        //private string _name;
        //private string _Id;
        //private string _TextColors = "#000000";
        //private string _Description;
        //private Picture _DefaultImage;



        private string _Id;
        public string Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private string _TextColors= "#000000";
        public string TextColors
        {
            get { return _TextColors; }
            set
            {
                if (_TextColors != value)
                {
                    _TextColors = value;
                    OnPropertyChanged("TextColors");
                }
            }
        }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private Picture _DefaultImage;
        public Picture DefaultImage
        {
            get { return _DefaultImage; }
            set
            {
                if (_DefaultImage != value)
                {
                    _DefaultImage = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private Picture _Images;
        public Picture Images
        {
            get { return _Images; }
            set
            {
                if (_Images != value)
                {
                    _Images = value;
                    OnPropertyChanged("Description");
                }
            }
        }


        private ObservableCollection<RecipeIngredient> _Ingredients;
        public ObservableCollection<RecipeIngredient> Ingredients
        {
            get { return _Ingredients; }
            set
            {
                if (_Ingredients != value)
                {
                    _Ingredients = value;
                    OnPropertyChanged("Ingredients");
                }
            }
        }


        //private ObservableCollection<RecipeIngredient> _Ingredients = new ObservableCollection<RecipeIngredient>();
        //public ObservableCollection<RecipeIngredient> Ingredients
        //{
        //    get => _Ingredients;
        //    set
        //    {
        //        _Ingredients = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
