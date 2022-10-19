﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.Core
{
    public static class HttpHelper
    {
        public static async Task Post<T>(string url, T contentValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public static async Task Put<T>(string url, T stringValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var content = new StringContent(JsonConvert.SerializeObject(stringValue), Encoding.UTF8, "application/json");
                var result = await client.PutAsync(url, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public static async Task<T> Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }

        public static async Task Delete(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var result = await client.DeleteAsync(url);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}
