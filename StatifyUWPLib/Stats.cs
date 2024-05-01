using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Media;

namespace StatifyUWPLib
{
    public class Stats
    {
        public async static Task<List<(string name, ImageSource image)>> artists(int limit, string range)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/me/top/artists?time_range={range}&limit={limit}&offset=0");
            
            DataPackage pkg = new DataPackage();
            pkg.SetText(Auth.AccessToken);
            Clipboard.SetContent(pkg);

            request.Headers.Add("Authorization", $"Bearer {Auth.AccessToken}");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            { await Auth.RefreshToken(); await CoreApplication.RequestRestartAsync("silly"); }
            response.EnsureSuccessStatusCode();
            List<(string name, ImageSource image)> list = new List<(string name, ImageSource image)>();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                JsonDocument jsonDocument = JsonDocument.Parse(jsonString);

                JsonElement root = jsonDocument.RootElement;

                JsonElement itemsArray = root.GetProperty("items");

                foreach (JsonElement item in itemsArray.EnumerateArray())
                {
                    string name = item.GetProperty("name").GetString();
                    JsonElement imagesArray = item.GetProperty("images");
                    string imageUrl = imagesArray[1].GetProperty("url").GetString();

                    list.Add((name, await Images.GetImageByURL(imageUrl)));
                }
                return list;
            }
            return null;


        }
    }
}
