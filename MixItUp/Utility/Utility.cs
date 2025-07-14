using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MixItUp.Utility
{
    public class Utility
    {
        /// <summary>
        /// TODO : To Generate Image through Base 64...
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Xamarin.Forms.ImageSource GetImageFromBase64(string base64)
        {
            try
            {
                byte[] Base64Stream = Convert.FromBase64String(base64);
                var image = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                return image;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// TODO : To Encode the Url...
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string EncodeUrlToBase64(string base64)
        {
            try
            {
                string url = base64;
                string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(url));

                using (var client = new WebClient())
                {
                    byte[] dataBytes = client.DownloadData(new Uri(url));
                    string encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                    return encodedFileAsBase64;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
