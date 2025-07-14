using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BFAPI;
using MixItUp.Model;
using MixItUp.Pages.RecipePage;
using Xamarin.Forms;

namespace MixItUp.CustomControls
{
    public class NativeListView : ListView
    {
        private readonly DateTime _dateTime = DateTime.Now;

        private static List<NativeListView> _views = new List<NativeListView>();

        public static readonly BindableProperty ScrollToItemProperty =
            BindableProperty.Create("ScrollToItem", typeof(object), typeof(NativeListView), new object(),
                BindingMode.TwoWay, propertyChanged: OnScrollToItemPropertyChanged);

        public static readonly BindableProperty ScrollToRecipeProperty =
            BindableProperty.Create("ScrollToRecipe", typeof(Recipe), typeof(NativeListView), new Recipe(),
                BindingMode.TwoWay, propertyChanged: OnScrollToRecipePropertyChanged);

        public static readonly BindableProperty SelectedCategoryProperty =
            BindableProperty.Create("SelectedCategory", typeof(string), typeof(NativeListView), "",
                BindingMode.TwoWay, propertyChanged: SelectedCategoryPropertyChanged);

        public static event Action ViewCellSizeChangedEvent;


        public object ScrollToItem
        {
            set
            {
                SetValue(ScrollToItemProperty, value);
                ScrollTo(value, ScrollToPosition.MakeVisible, true);
            }
        }

        public object ScrollToRecipe
        {
            set
            {
                SetValue(ScrollToRecipeProperty, value);
                ScrollTo(value, ScrollToPosition.MakeVisible, true);
            }
        }

        public object SelectedCategory
        {
            get { return GetValue(SelectedCategoryProperty); }
            set
            {
                SetValue(SelectedCategoryProperty, value);
            }
        }

        private static ObservableCollection<Recipe> _items = new ObservableCollection<Recipe>();
        private static void OnScrollToItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && !string.IsNullOrEmpty((string)newValue))
            {
                var selectedItem = (string)newValue;

                foreach (var v in _views.Where(x => x.SelectedCategory.ToString() == _selectedCategory))
                {
                    if (selectedItem.ToUpper() != "#")
                    {
                        var items = (ObservableCollection<Recipe>)v.ItemsSource;
                        object prod = items.FirstOrDefault(x => x.Name.ToUpper().StartsWith(selectedItem.ToUpper()));
                        if (prod != null) v.ScrollTo(prod, ScrollToPosition.Start, false);
                        //v.BackgroundColor = GetNextColor();
                    }
                    else
                    {
                        var items = (ObservableCollection<Recipe>)v.ItemsSource;
                        object prod = items.FirstOrDefault();
                        if (prod != null) v.ScrollTo(prod, ScrollToPosition.Start, false);
                    }
                    v.ScrollToItem = null;
                }
            }
        }

        private static void OnScrollToRecipePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                foreach (var v in _views.Where(x => x.SelectedCategory.ToString() == _selectedCategory))
                {
                    v.ScrollTo(newValue, ScrollToPosition.Start, false);
                }
            }
        }

        private static string _selectedCategory;
        private static void SelectedCategoryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            _selectedCategory = (string)newValue;

            for (var i = _views.Count - 1; i >= 0; i--)
            {
                if (!_views[i].IsVisible)
                {
                    _views.RemoveAt(i);
                }
            }
        }

        private static int _lastColor = -1;
        private static Color GetNextColor()
        {
            _lastColor++;
            switch (_lastColor)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Orange;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Pink;
                case 4:
                    return Color.Purple;
                case 5:
                    return Color.Blue;
                default:
                    _lastColor = -1;
                    return Color.Transparent;
            }
        }


        public NativeListView()
        {
            base.BindingContextChanged += (sender, args) =>
            {
                _views.Add(this);
            };
        }
    }
}
