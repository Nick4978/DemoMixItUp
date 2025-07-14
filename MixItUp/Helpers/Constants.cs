using MixItUp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using BFAPI;
using System.Collections.ObjectModel;

namespace MixItUp.Helpers
{
    public class Constants
    {
        public static bool IsCartPage = false;
        public static bool IsRecipePage = false;
        public static bool IsCabinetPage = false;
        public static bool IsUserProfilePage = false;
        public static bool IsRecipeDetailPage = false;
        public static bool IsRecipeDetail = false;
        public static bool IsNewRecipePage = false;
        public static bool IsMyRecipesPage = false;

        public static Recipe RecipeDetail = new Recipe();

        //TODO : To maintain variables for Auth Login
        public static bool isAuthLogin = false;
        public static string AuthUserName = string.Empty;
        public static string imgFilePath = string.Empty;
        public static string AuthUserEmail = string.Empty;
        public static string AuthUserPhone = string.Empty;
        public static string AuthUserFName = string.Empty;
        public static string AuthUserLName = string.Empty;
        public static string AuthUserProfilePic = string.Empty;
        public static string AuthUserCountryId = string.Empty;
        public static string AuthUserPassword = "1234";
        public static int fblogin = 0;
        public static int AuthPageGoogle = 0;


        public static ObservableCollection<NewRecipeModel> permanentRecipeLists = new ObservableCollection<NewRecipeModel>();
        public static ObservableCollection<NewRecipeModel> permanentRecipe = new ObservableCollection<NewRecipeModel>();


        public static List<string> lstCategories = new List<string>();


    }
}
