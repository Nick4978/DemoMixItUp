using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.Cabinet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CabinetPage : ContentPage
	{
        protected CabinetPageVM CabinetPageVM;
		public CabinetPage ()
		{
			InitializeComponent ();
            CabinetPageVM = new CabinetPageVM(this.Navigation);
            this.BindingContext = CabinetPageVM;
            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        private void LvNews_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

        private void mastermenu(object sender, EventArgs e)
        {
            Helpers.Constants.IsCabinetPage = true;
            App.Current.MainPage = new Pages.MasterPage.MasterPage();
            //App.masterDetailPage.IsPresented = true;
        }
    }
}