
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MixItUp.GoogleAuthentication
{
    public class GoogleService
    {
        public async Task<string> GetEmailAsync(string tokenType, string accessToken)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                //var json = await httpClient.GetStringAsync("https://www.googleapis.com/auth/userinfo.profile");
                var json = await httpClient.GetStringAsync("https://www.googleapis.com/oauth2/v1/userinfo?alt=json");
                System.Diagnostics.Debug.WriteLine("json responce-------------**************************-----------------" + json);
                var googleResponse = JsonConvert.DeserializeObject<GoogleResponseModel>(json);

                //string[] fullname = googleResponse.name.Split(' ');
                //if (fullname.Count() > 0)
                //{
                //    Helpers.Constants.AuthUserFName = (fullname[0]);
                //    Helpers.Constants.AuthUserLName = (fullname[1]);
                //}
                //else
                //{
                //    Helpers.Constants.AuthUserFName = googleResponse.name;
                //    Helpers.Constants.AuthUserLName = string.Empty;
                //}
                Helpers.Constants.AuthUserEmail = googleResponse.email;
                Helpers.Constants.AuthUserPhone = string.Empty;
                Helpers.Constants.AuthUserProfilePic = googleResponse.picture;

                Helpers.Settings.GeneralAuthUserEmail = googleResponse.email;
                Helpers.Settings.GeneralAuthUserName = googleResponse.name;
                Helpers.Settings.GeneralAuthUserProfilePic = googleResponse.picture;
                Helpers.Settings.GeneralLoginLable = "Logout";
                Helpers.Settings.GeneralSocialLogin = "True";

                return googleResponse.email;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class GoogleResponseModel
    {
        public string id { get; set; }
        public string email { get; set; }
        public bool verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string link { get; set; }
        public string picture { get; set; }
        public string locale { get; set; }
    }
}
