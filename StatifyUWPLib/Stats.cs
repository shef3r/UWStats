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

using static UWStatsUWPLib.SettingsProvider;

namespace UWStatsUWPLib
{
    public class Stats
    {
        public async static Task<List<Artist>> artists(int limit, string range)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/me/top/artists?time_range={range}&limit={limit}&offset=0");
            request.Headers.Add("Authorization", $"Bearer {AccessToken}");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            { await Auth.RefreshToken(); await CoreApplication.RequestRestartAsync("silly"); }
            response.EnsureSuccessStatusCode();
            List<Artist> list = new List<Artist>();
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
                    string imageUrl = imagesArray[2].GetProperty("url").GetString();

                    list.Add(
                        new Artist()
                        {
                            Name = name,
                            Image = await Images.GetImageByURL(imageUrl)
                        }
                    );
                }
                return list;
            }
            return null;
        }

        public async static Task<List<Track>> tracks(int limit, string range)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/me/top/tracks?time_range={range}&limit={limit}&offset=0");
            request.Headers.Add("Authorization", $"Bearer {AccessToken}");
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            { await Auth.RefreshToken(); await CoreApplication.RequestRestartAsync("silly"); }
            response.EnsureSuccessStatusCode();
            List<Track> list = new List<Track>();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                JsonDocument jsonDocument = JsonDocument.Parse(jsonString);

                JsonElement root = jsonDocument.RootElement;

                JsonElement itemsArray = root.GetProperty("items");

                foreach (JsonElement item in itemsArray.EnumerateArray())
                {
                    JsonElement artistsArray = item.GetProperty("artists");
                    JsonElement albumArray = item.GetProperty("album");
                    JsonElement imagesArray = albumArray.GetProperty("images");

                    string artist = artistsArray[0].GetProperty("name").GetString();
                    string imageUrl = imagesArray[2].GetProperty("url").GetString();
                    string album = albumArray.GetProperty("name").GetString();
                    string name = item.GetProperty("name").GetString();

                    list.Add(new Track() { 
                        Name = name,
                        Album = album, 
                        Artist = artist, 
                        Cover = await Images.GetImageByURL(imageUrl)
                    });
                }
                return list;
            }
            return null;
        }
    }
}