using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MixItUp.Pages
{
    public class BaseViewModel : BindableObject
    {
        private INavigation _Navigation;       
        public INavigation Navigation
        {
            get { return _Navigation; }
            set
            {
                if (_Navigation != value)
                {
                    _Navigation = value;
                    OnPropertyChanged("Navigation");
                }
            }
        }

        public bool IsInitialized { get; set; }

        public BaseViewModel(INavigation navigation = null)
        {
            try
            {
                Navigation = navigation;
               // _businessCode = SimpleIoc.Default.GetInstance<IBusinessCode>();
            }
            catch (Exception ex)
            { }
        }
        public Acr.UserDialogs.IUserDialogs UserDialog
        {
            get
            {
                return Acr.UserDialogs.UserDialogs.Instance;
            }
        }
        public async Task PushModalAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            if (Navigation != null)
                await Navigation.PopModalAsync();
        }

        public async Task PushAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushModalAsync(page);
        }

        public async Task PopAsync()
        {
            if (Navigation != null)
                await Navigation.PopModalAsync();
        }
    }
}
