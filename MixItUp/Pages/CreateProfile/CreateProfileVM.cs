using Acr.UserDialogs;
using MixItUp.Common;
using MixItUp.Interface;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MixItUp.Pages.CreateProfile
{
    public class CreateProfileVM : BaseViewModel
    {
        //To define the class lable variable...

        //To define the constructor...
        public CreateProfileVM(INavigation _nav)
        {
            Navigation = _nav;
            BackCommand = new DelegateCommand(BackCommandAsync);
            TakePhotoCommand = new DelegateCommand(TakePhotoCommandAsync);
            CreateAccountCommand = new DelegateCommand(CreateAccountCommandAsync);
        }

        #region Delegate command
        public DelegateCommand CreateAccountCommand { get; }
        public DelegateCommand BackCommand { get; }
        public DelegateCommand TakePhotoCommand { get; }
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
        /// To define the back command ...
        /// </summary>
        /// <param name="obj"></param>
        private async void BackCommandAsync(object obj)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// To define the create account command...
        /// </summary>
        /// <param name="obj"></param>
        private async void CreateAccountCommandAsync(object obj)
        {
            //Apply Validations..
            if (!await Validate()) return;
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

        /// <summary>
        /// To define the take photo command...
        /// </summary>
        /// <param name="obj"></param>
        private async void TakePhotoCommandAsync(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //Ask the user if they want to use the camera or pick from the gallery
                var action = await UserDialogs.Instance.ActionSheetAsync("Add Photo", "Cancel", "", null, "Choose Existing", "Take Photo");

                if ((action == "Take Photo"))
                {
                    try
                    {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync
                                                     (Plugin.Permissions.Abstractions.Permission.Camera);

                        if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            var result = await CrossPermissions.Current.RequestPermissionsAsync(new[] {
                                                                                  Plugin.Permissions.Abstractions.Permission.Camera });
                            status = result[Plugin.Permissions.Abstractions.Permission.Camera];
                        }

                        if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            if (!CrossMedia.Current.IsCameraAvailable)
                            {
                                UserDialogs.Instance.Alert("No camera avaialble.", null, "OK");
                                return;
                            }
                            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.png"
                            });
                            if (file == null)
                                return;

                            Helpers.Constants.imgFilePath = file.Path;
                            ProfileImg = file.Path;
                            var ImageByteData = await DependencyService.Get<IMediaService>().ResizeImage(await DependencyService.Get<IMediaService>().GetMediaInBytes(file.Path), 120, 120);
                            ProfileImgBase64 = Convert.ToBase64String(ImageByteData);

                            Xamarin.Forms.MessagingCenter.Send<string>("", "UpdateProfileImage");
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                    catch (Exception exception)
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else if ((action == "Choose Existing"))
                {
                    try
                    {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync
                                                     (Plugin.Permissions.Abstractions.Permission.Photos);

                        if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            var result = await CrossPermissions.Current.RequestPermissionsAsync(new[] {
                                                                                  Plugin.Permissions.Abstractions.Permission.Photos });
                            status = result[Plugin.Permissions.Abstractions.Permission.Photos];
                        }

                        if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file == null)
                            {
                                return;
                            }

                            Helpers.Constants.imgFilePath = file.Path;
                            ProfileImg = file.Path;
                            var ImageByteData = await DependencyService.Get<IMediaService>().ResizeImage(await DependencyService.Get<IMediaService>().GetMediaInBytes(file.Path), 120, 120);
                            ProfileImgBase64 = Convert.ToBase64String(ImageByteData);

                            Xamarin.Forms.MessagingCenter.Send<string>("", "UpdateProfileImage");
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                    catch (Exception exception)
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                }
            });
        }
        #endregion
    }
}
