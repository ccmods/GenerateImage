using System;
using System.Net;
using Entity;
using Newtonsoft.Json;
using Serilog;

namespace BC {
      internal class WebBC {
        public static WeatherRoot getWeatherData(string apiUrl, string apiKey) {
            var weatherJson = string.Format(apiUrl, apiKey);
            try {
                WebClient srcJson = new WebClient();
                string jsonData = srcJson.DownloadString(weatherJson);
                var jsonObj = JsonConvert.DeserializeObject<WeatherRoot>(jsonData);
                return jsonObj;
            } catch (Exception ex) {
                Log.Error(ex.ToString());
            }
            return null;
        }
        
        public static string getBackgrndImgUrl(string tarUrl) {
            string imgUrl = string.Empty;
            try {
                var srcJson = new WebClient();
                string jsonData = srcJson.DownloadString(tarUrl);
                var jsonObj = JsonConvert.DeserializeObject<Root>(jsonData);
                Item imgObj = jsonObj.batchrsp.items[0];
                ItemRoot innerObj = JsonConvert.DeserializeObject<ItemRoot>(imgObj.item);
                imgUrl= innerObj.ad.image_fullscreen_001_landscape.u;
            } catch (Exception ex) {
                Log.Error(ex.ToString());
            }
            return imgUrl;            
        }
    }
}