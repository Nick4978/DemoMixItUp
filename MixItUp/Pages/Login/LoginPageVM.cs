using Acr.UserDialogs;
using MixItUp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MixItUp.Pages.Login
{
    public class LoginPageVM : BaseViewModel
    {
        //To define the class lable variable...

        //To define the constructor...
        public LoginPageVM(INavigation _nav)
        {
            Navigation = _nav;
            LoginCommand = new DelegateCommand(LoginCommandAsync);
            BackCommand = new DelegateCommand(BackCommandAsync);
            CreateProfileCommnad = new DelegateCommand(CreateProfileCommnadAsync);
        }



        #region Delegate command
        public DelegateCommand LoginCommand { get; }
        public DelegateCommand CreateProfileCommnad { get; }
        public DelegateCommand BackCommand { get; }
        #endregion

        #region Properties
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }


        #endregion

        #region Method 
        /// <summary>
        /// To Define the login button command ...
        /// </summary>
        /// <param name="obj"></param>
        private async void LoginCommandAsync(object obj)
        {
            //Apply Validations...
            if (!await Validate()) return;
        }

        /// <summary>
        /// To define the create profile button command...
        /// </summary>
        /// <param name="obj"></param>
        private async void CreateProfileCommnadAsync(object obj)
        {
            await Navigation.PushModalAsync(new Pages.CreateProfile.CreateProfilePage());
        }

        /// <summary>
        /// To define the back command ...
        /// </summary>
        /// <param name="obj"></param>
        private async void BackCommandAsync(object obj)
        {
            await Navigation.PopModalAsync();
        }

        //To Validate all User Input Fields...
        private async Task<bool> Validate()
        {
            UserDialog.ShowLoading();

            if (string.IsNullOrEmpty(Email))
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Please enter your email.");
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Please enter your password.");
                return false;
            }

            return true;
        }
        #endregion
    }
}
