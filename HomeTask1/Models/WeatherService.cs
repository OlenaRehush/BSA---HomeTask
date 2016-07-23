﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace HomeTask1.Models
{
    public interface IWeatherService
    {
        string linkFormat { get; set; }
        Task<T> GetWeather<T>(string city);
        
    }
    public class WeatherService:IWeatherService
    {
        //private string linkFormat;

        
        public async Task<T> GetWeather<T>(string city)
        {   
            
                // ... Use HttpClient.
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = client.GetAsync(String.Format(linkFormat, city)).Result)
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(result);
                }
            
            
        }

        public string linkFormat { get; set; }
    }
    
}