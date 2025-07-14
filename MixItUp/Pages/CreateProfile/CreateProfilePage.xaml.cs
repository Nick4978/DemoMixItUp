using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MixItUp.Pages.CreateProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfilePage : ContentPage
    {
        //To define the class lable variable...
        protected CreateProfileVM createProfileVM;

        //To define the constructor...
        public CreateProfilePage()
        {
            InitializeComponent();
            createProfileVM = new CreateProfileVM(this.Navigation);
            this.BindingContext = createProfileVM;

            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            MessagingCenter.Subscribe<string>("", "UpdateProfileImage", (sender) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (createProfileVM.ProfileImg != null)
                    {
                        ImgProfile.Source = createProfileVM.ProfileImg;
                        // ImgLeaveRequests.Source = Utility.Utility.GetImageFromBase64(leaveRequestDetailPageVM.ImgLeaveRequest); 
                    }
                });
            });
        }

        #region Event Handler
        private void MasterMenu(object sender, EventArgs e)
        {
            App.masterDetailPage.IsPresented = true;
        }
        #endregion

    }
}