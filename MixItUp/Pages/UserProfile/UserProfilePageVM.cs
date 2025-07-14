using MixItUp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MixItUp.Pages.UserProfile
{
    public class UserProfilePageVM : BaseViewModel
    {
        public UserProfilePageVM(INavigation _nav)
        {
            Navigation = _nav;
            EditProfileCommand = new DelegateCommand(EditProfileCommandAsync);
        }

        #region Delegate Command
        public DelegateCommand EditProfileCommand { get; }
        #endregion

        #region Properties

        private string _Reviews = "20";
        public string Reviews
        {
            get { return _Reviews; }
            set
            {
                if (_Reviews != value)
                {
                    _Reviews = value;
                    OnPropertyChanged("Reviews");
                }
            }
        }
        private string _UserName = "test user";
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged("UserName");
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
        private string _Email = "testuser@gmail.com";
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

        private string _Login = "Login";
        public string Login
        {
            get { return _Login; }
            set
            {
                if (_Login != value)
                {
                    _Login = value;
                    OnPropertyChanged("Login");
                }
            }
        }


        private bool _IsEditProfileImg = false;
        public bool IsEditProfileImg
        {
            get { return _IsEditProfileImg; }
            set
            {
                if (_IsEditProfileImg != value)
                {
                    _IsEditProfileImg = value;
                    OnPropertyChanged("IsEditProfileImg");
                }
            }
        }


        #endregion

        #region Method
        /// <summary>
        /// To define the edit profile command ...
        /// </summary>
        /// <param name="obj"></param>
        private async void EditProfileCommandAsync(object obj)
        {
            await Navigation.PushModalAsync(new Pages.CreateProfile.EditProfilePage());
        }
        #endregion
    }
}
