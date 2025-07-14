using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        //To define the class lable variable...
        protected LoginPageVM loginPageVM;
        //To define the constructor...
        public LoginPage()
        {
            InitializeComponent();
            loginPageVM = new LoginPageVM(this.Navigation);
            this.BindingContext = loginPageVM;
        }

        #region Event Handler
        #endregion
    }
}