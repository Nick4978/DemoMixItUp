using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;
using Xamarin.Forms;

namespace MixItUp.GoogleAuthentication
{
    public class GoogleAuthenticator
    {
        private const string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        private const string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        private const bool IsUsingNativeUI = true;

        private OAuth2Authenticator _auth;
        private IGoogleAuthenticationDelegate _authenticationDelegate;

        public GoogleAuthenticator(string clientId, string scope, string redirectUrl, IGoogleAuthenticationDelegate authenticationDelegate)
        {
            _authenticationDelegate = authenticationDelegate;

            _auth = new OAuth2Authenticator(clientId, string.Empty, scope,
                                            new Uri(AuthorizeUrl),
                                            new Uri(redirectUrl),
                                            new Uri(AccessTokenUrl),
                                            null, IsUsingNativeUI);

            _auth.Completed += OnAuthenticationCompleted;
            //  _auth.Error += OnAuthenticationFailed;
            _auth.Error += (sender, eventArgs) =>
            {
                OAuth2Authenticator auth2 = (OAuth2Authenticator)sender;
                auth2.ShowErrors = false;
                auth2.OnCancelled();
                auth2.AllowCancel = false;
            };
        }

        public OAuth2Authenticator GetAuthenticator()
        {
            return _auth;
        }

        public void OnPageLoading(Uri uri)
        {
            _auth.OnPageLoading(uri);
        }

        private void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (e.IsAuthenticated)
                {
                    var token = new GoogleOAuthToken
                    {
                        TokenType = e.Account.Properties["token_type"],
                        AccessToken = e.Account.Properties["access_token"]
                    };

                    DependencyService.Get<IMessage>().LongAlert("Please wait...");

                    _authenticationDelegate.OnAuthenticationCompleted(token);
                }
                else
                {
                    _authenticationDelegate.OnAuthenticationCanceled();
                }
            });
        }
    }
}
