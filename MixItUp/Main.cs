using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using BFAPI;
using Microsoft.Data;
using TCDataU;
using TCDataU.Databases;

namespace MixItUp
{
    public class MasterRecipeChangedEventArgs : EventArgs
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public Recipe Recipe { get; set; }

        public MasterRecipeChangedEventArgs(ObservableCollection<Recipe> recipes)
        {
            foreach (var recp in recipes)
            {
                if (recp.Images != null)
                {
                    recp.DefaultImage = recp.Images[0];
                }
                Recipes = recipes;
            }
            //Recipes = recipes;
        }

        public MasterRecipeChangedEventArgs(Recipe recipe)
        {
            Recipe = recipe;
        }
    }

    public static class Main
    {
        public enum ConvertTypes
        {
            Parts,
            Milliliters,
            Ounces,
        }

        public static List<Category> Categories { get; set; } = new List<Category>();
        public static List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public static List<Cabinet> Cabinets { get; set; } = new List<Cabinet>();
        public static ObservableCollection<CabinetIngredient> CabinetIngredients { get; set; } = new ObservableCollection<CabinetIngredient>();
        
        public static ObservableCollection<Recipe> MasterRecipeList { get; set; } = new ObservableCollection<Recipe>();
        public static Cabinet SelectedCabinet;
        
        public static string DbPath;


        public static SqlLite<T> GetConnection<T>() where T : new()
        {
            var path = string.IsNullOrEmpty(DbPath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Barfriender.db")
                : DbPath;
            return new SqlLite<T>(path);
        }

        /*public static LoginInfo GetDataLogin()
        {
            return GetDataLogin(false);
        }

        public static SqlLite GetConnection()
        {
            return new SqlLite(GetDataLogin(false));
        }*/

        /*        public static LoginInfo GetDataLogin(bool useMasterTable)
                {
                    return
                        new LoginInfo
                        {
                            Server = string.IsNullOrEmpty(DbPath) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Barfriender.db") : DbPath,
                            Database = string.IsNullOrEmpty(DbPath) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Barfriender.db") : DbPath,
                            UseTrustedConnection = false,
                            DatabaseType = TCDataU.Main.DataBaseTypes.SqlLite,
                            UseSql11Client = false,
                        };
                }*/

        public static List<RecipeIngredient> ConvertTo(ConvertTypes type, Recipe.GlassTypes glass, double baseFactor, bool requiresIce, List<RecipeIngredient> ingredients)
        {

            if (baseFactor == 0)
                baseFactor =
                    ingredients.FirstOrDefault(x => x.QuantityType == RecipeIngredient.QuantityTypes.Part) == null
                        ? ingredients.Select(x => x.Quantity).Min()
                        : 1;

            var results = new List<RecipeIngredient>();
            switch (type)
            {
                case ConvertTypes.Parts:
                    results = BFAPI.Main.ConvertTo(ingredients, glass,
                        BFAPI.Main.ConversionTypes.Parts, baseFactor, requiresIce);
                    break;
                case ConvertTypes.Milliliters:
                    results = BFAPI.Main.ConvertTo(ingredients, glass,
                        BFAPI.Main.ConversionTypes.Milliliters, baseFactor, requiresIce);
                    break;
                case ConvertTypes.Ounces:
                    results = BFAPI.Main.ConvertTo(ingredients, glass,
                        BFAPI.Main.ConversionTypes.Ounces, baseFactor, requiresIce);
                    break;
            }

            return results;
        }
    }
}

