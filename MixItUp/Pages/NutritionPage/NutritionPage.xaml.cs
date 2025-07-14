using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFAPI;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.NutritionPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NutritionPage : ContentPage
	{
		public NutritionPage(Recipe recipe)
		{
			InitializeComponent();
            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            if (recipe.Nutrition.Count == 0)
            {
                NutritionGrid.IsVisible = false;
                NoData.IsVisible = true;
            }
            BindingContext = recipe;
        }

        private async void mastermenu(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}