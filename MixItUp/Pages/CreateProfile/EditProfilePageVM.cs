using Acr.UserDialogs;
using MixItUp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MixItUp.Pages.CreateProfile
{
    public class EditProfilePageVM : BaseViewModel
    {
        //To define the class lable variable...

        //To define the constructor...
        public EditProfilePageVM(INavigation _nav)
        {
            Navigation = _nav;
            SaveChangesCommand = new DelegateCommand(SaveChangesCommandAsync);
            BackCommand = new DelegateCommand(BackCommandAsync);
        }


        #region Delegate Command
        public DelegateCommand SaveChangesCommand { get; }
        public DelegateCommand BackCommand { get; }
        #endregion

        #region Properties 

        private string _YourName;
        public string YourName
        {
            get { return _YourName; }
            set
            {
                if (_YourName != value)
                {
                    _YourName = value;
                    OnPropertyChanged("YourName");
                }
            }
        }

        private string _ProfileImg = "profile_noimage.png";
        public string ProfileImg
        {
            get { return _ProfileImg; }
            set
            {
                if (_ProfileImg != value)
                {
                    _ProfileImg = value;
                    OnPropertyChanged("ProfileImg");
                }
            }
        }

        private string _ProfileImgBase64;
        public string ProfileImgBase64
        {
            get { return _ProfileImgBase64; }
            set
            {
                if (_ProfileImgBase64 != value)
                {
                    _ProfileImgBase64 = value;
                    OnPropertyChanged("ProfileImgBase64");
                }
            }
        }

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
        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set
            {
                if (_ConfirmPassword != value)
                {
                    _ConfirmPassword = value;
                    OnPropertyChanged("ConfirmPassword");
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

        private string _Gender;
        public string Gender
        {
            get { return _Gender; }
            set
            {
                if (_Gender != value)
                {
                    _Gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }


        #endregion

        #region Method
        /// <summary>
        /// To define the save changes button command...
        /// </summary>
        /// <param name="obj"></param>
        private async void SaveChangesCommandAsync(object obj)
        {
            //Apply Validations..
            if (!await Validate()) return;
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
            if (string.IsNullOrEmpty(YourName))
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Please enter your name.");
                return false;
            }
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
            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Please enter your confirm password.");
                return false;
            }
            if (Password != ConfirmPassword)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Your password and confirm password does not match.");
                return false;
            }
            if (string.IsNullOrEmpty(Gender))
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Please enter your gender.");
                return false;
            }
            return true;
        }
        #endregion
    }
}
