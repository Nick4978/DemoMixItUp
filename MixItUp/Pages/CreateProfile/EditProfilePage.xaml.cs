using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.CreateProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        //To define the class lable variable...
        protected EditProfilePageVM editProfilePageVM;
        //To define the constructor...
        public EditProfilePage()
        {
            InitializeComponent();
            editProfilePageVM = new EditProfilePageVM(this.Navigation);
            this.BindingContext = editProfilePageVM;
        }

        #region Event Handler
        #endregion
    }
}