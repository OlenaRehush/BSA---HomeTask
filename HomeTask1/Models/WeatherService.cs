using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace HomeTask1.Models
{
    public class WeatherService
    {
        private string linkFormat;

        public WeatherService(string linkFormat)
        {
            this.linkFormat = linkFormat;
        }

        public T GetWeather<T>(string city)
        {

            
            {
                // ... Use HttpClient.
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = client.GetAsync(String.Format(linkFormat, city)).Result)
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
            
        }
    }
    
}