using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
using Windows.UI.Xaml.Media;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Net;
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using System.Runtime.InteropServices.WindowsRuntime;

namespace StatifyUWPLib
{
    public class Images
    {
        public static async Task<ImageSource> GetProfilePic()
        {
            if (File.Exists(Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\pfp.png"))
            {
                StorageFile file = await StorageFile.GetFileFromPathAsync(Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\pfp.png");
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(fileStream);
                    return bitmapImage;
                }
            }
            else
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me");
                request.Headers.Add("Authorization", $"Bearer {Auth.AccessToken}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string resp = await response.Content.ReadAsStringAsync();
                JsonNode node = JsonNode.Parse(resp);
                Uri imgUri = new Uri(node["images"][0]["url"].ToString());
                (new WebClient()).DownloadFile(imgUri, Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\pfp.png");
                StorageFile file = await StorageFile.GetFileFromPathAsync(Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\pfp.png");
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(fileStream);
                    return bitmapImage;
                }
            }
        }

        public static async Task<ImageSource> GetImageByURL(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] imageBytes = await client.GetByteArrayAsync(new Uri(imageUrl));

                InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
                await randomAccessStream.WriteAsync(imageBytes.AsBuffer());
                randomAccessStream.Seek(0);

                BitmapImage bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(randomAccessStream);
                return bitmapImage;
            }
        }
    }
}
