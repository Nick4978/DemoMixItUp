using MixItUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BFAPI;
using Xamarin.Forms;

namespace MixItUp.Pages.RecipeDetail
{
    public class RecipeDetailVM : BaseViewModel
    {
        private Recipe _recipe = null;

        public RecipeDetailVM(INavigation _Nav, Recipe recipe)
        {
            Navigation = _Nav;
            RecipeName = recipe.Name;

            ReviewDetail = new ObservableCollection<ReviewModel>
            {
                new ReviewModel{Name="NICA", Review="Lorel Ipsum is simply Dummy Text of printing And typesetting Industry.Lorel Ipsum has been Industy Standard."},
                new ReviewModel{Name="MARIA", Review="Lorel Ipsum is simply Dummy Text of printing And typesetting Industry.Lorel Ipsum has been Industy Standard."}

            };

            var _newIngredients = new List<RecipeIngredient>();
            foreach (var i in recipe.Ingredients)
            {
                _newIngredients.Add(new RecipeIngredient(Main.GetConnection<RecipeIngredient>())
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity,
                    QuantityType = i.QuantityType,
                });
            }

            _newIngredients = Main.ConvertTo(Main.ConvertTypes.Ounces, recipe.Glass, recipe.ConversionBase,
                recipe.RequiresIce, _newIngredients);

            RecipeDetail = new ObservableCollection<RecipeDetailModel>();
            foreach (var i in _newIngredients.OrderBy(x => x.Type).ThenByDescending(x => x.Quantity).ThenBy(x => x.Name))
            {
                var qtyString = i.Quantity.ToString();

                var parts = qtyString.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 1)
                {
                    var firstPart = parts[0];
                    var secondPart = "";
                    switch (parts[1])
                    {
                        case "25":
                            secondPart = "¼";
                            break;
                        case "33":
                        case "3":
                            secondPart = "⅓";
                            break;
                        case "5":
                        case "50":
                            secondPart = "½";
                            break;
                        case "75":
                            secondPart = "¾";
                            break;
                        default:
                            secondPart = "." + parts[0];
                            break;
                    }

                    if (firstPart == "0")
                    {
                        qtyString = secondPart;
                    }
                    else
                    {
                        qtyString = firstPart + secondPart; //(!secondPart.Contains(".")) ? firstPart + " " + secondPart : firstPart + secondPart;
                    }
                }

                var recipeInCabinet = new CabinetIngredient(Main.GetConnection<CabinetIngredient>())
                                          .GetAll(Main.SelectedCabinet.Id)
                                          .FirstOrDefault(x => x.IngredientId == i.IngredientId) != null;

                RecipeDetail.Add(new RecipeDetailModel() {Id = i.IngredientId, Quantity = qtyString + " oz", Ingredient = i.Name, InCabinet = recipeInCabinet});
            }

            if (recipe.Instructions.Count > 1)
            {
                foreach (var r in recipe.Instructions.OrderBy(x => x.Step).ToList())
                {
                    Instructions += $"{r.Step}. {r.Text}\r\n\r\n";
                }
            }
            else
            {
                Instructions = recipe.Instructions[0].Text + "\r\n\r\n";
            }

            Instructions = Instructions.Substring(0, Instructions.LastIndexOf("\r\n\r\n"));

            DefaultImage = recipe.Images[0];
        }

        private void ConvertTo(Main.ConvertTypes type)
        {
            var ingredients = Main.ConvertTo(type, _recipe.Glass, _recipe.ConversionBase,
                _recipe.RequiresIce, _recipe.Ingredients);

            var qtyDescription = "parts";
            switch (type)
            {
                case Main.ConvertTypes.Milliliters:
                    qtyDescription = "ml";
                    break;
                case Main.ConvertTypes.Ounces:
                    qtyDescription = "oz";
                    break;
            }

            RecipeDetail = new ObservableCollection<RecipeDetailModel>();
            foreach (var i in ingredients)
            {
                var recipeInCabinet = new CabinetIngredient(Main.GetConnection<CabinetIngredient>())
                                          .GetAll(Main.SelectedCabinet.Id)
                                          .FirstOrDefault(x => x.IngredientId == i.IngredientId) != null;
                RecipeDetail.Add(new RecipeDetailModel() { Quantity = i.Quantity + $" {qtyDescription}", Ingredient = i.Name, InCabinet = recipeInCabinet });
            }
        }

        #region properties

        public string RecipeName { get; set; } = "";
        public string Instructions { get; set; } = "";
        public Picture DefaultImage { get; set; }

        private ObservableCollection<RecipeDetailModel> _recipeDetail;
        public ObservableCollection<RecipeDetailModel> RecipeDetail
        {
            get { return _recipeDetail; }
            set
            {
                if (_recipeDetail != value)
                {
                    _recipeDetail = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<ReviewModel> _ReviewDetail;
        public ObservableCollection<ReviewModel> ReviewDetail
        {
            get { return _ReviewDetail; }
            set
            {
                if (_ReviewDetail != value)
                {
                    _ReviewDetail = value;
                    OnPropertyChanged("ReviewDetail");
                }
            }
        }
        #endregion



    }
}
