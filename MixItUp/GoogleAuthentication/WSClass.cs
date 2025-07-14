
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MixItUp.GoogleAuthentication
{
    public class WSClass
    {
        async public static Task<List<string>> PostData(string Str_Uri, string Str_Param)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uristring = new Uri(Str_Uri);
                StringContent stringContent = new StringContent(Str_Param.Trim(), UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uristring, stringContent);
                var content = await response.Content.ReadAsStreamAsync();
                var rawJson = new StreamReader(content).ReadToEnd();
                if (rawJson == "Error")
                {
                    rawJson = "{\"result\":\"No Record Found\",\"status\":\"0\"}";

                }
                return await Task.Run(() => new List<string>() { rawJson.ToString() });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        async public static Task<List<string>> GetData(string Str_Uri)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uristring = new Uri(Str_Uri);
                HttpResponseMessage response = await client.GetAsync(uristring);
                var content = await response.Content.ReadAsStreamAsync();
                var rawJson = new StreamReader(content).ReadToEnd();
                if (rawJson == "Error")
                {
                    rawJson = "{\"result\":\"No Record Found\",\"status\":\"0\"}";
                }
                return await Task.Run(() => new List<string>() { rawJson.ToString() });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
