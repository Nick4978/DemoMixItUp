using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.ComponentModel;

namespace MixItUp.Helpers
{
    public class Settings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static Settings settings;


        public static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        public static Settings Current
        {
            get { return settings ?? (settings = new Settings()); }
        }

        #region Setting Constants

        private const string SubscriptionKey = "subscription_key";
        private static readonly string SubscriptionDefault = string.Empty;

        private const string Token = "Token";
        private static readonly string TokenDefault = string.Empty;

        private const string AuthUserEmail = "AuthUserEmail";
        private static readonly string AuthUserEmailDefault = string.Empty;

        private const string AuthUserName = "AuthUserName";
        private static readonly string AuthUserNameDefault = string.Empty;

        private const string AuthUserProfilePic = "AuthUserProfilePic";
        private static readonly string AuthUserProfilePicDefault = string.Empty;

        private const string LoginLable = "LoginLable";
        private static readonly string LoginLableDefault = "Login";

        private const string SocialLogin = "SocialLogin";
        private static readonly string SocialLoginDefault = string.Empty;


        #endregion
        public static string GeneralLoginLable
        {
            get
            {
                return AppSettings.GetValueOrDefault(LoginLable, LoginLableDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LoginLable, value);
            }
        }
        public static string GeneralToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(Token, TokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(Token, value);
            }
        }
        public static string GeneralAuthUserEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthUserEmail, AuthUserEmailDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthUserEmail, value);
            }
        }

        public static string GeneralAuthUserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthUserName, AuthUserNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthUserName, value);
            }
        }

        public static string GeneralAuthUserProfilePic
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthUserProfilePic, AuthUserProfilePicDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthUserProfilePic, value);
            }
        }
        public static string SubscriptionSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SubscriptionKey, SubscriptionDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SubscriptionKey, value);
            }
        }
        public static string GeneralSocialLogin
        {
            get
            {
                return AppSettings.GetValueOrDefault(SocialLogin, SocialLoginDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SocialLogin, value);
            }
        }
    }
}
