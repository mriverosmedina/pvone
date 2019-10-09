using Newtonsoft.Json;
using Plugin.Connectivity;
using pvone.Common.Models;
using pvone.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace pvone.Service
{
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.TurnOnInternet,
                };
            }

            bool isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.NoInternet,
                };
            }
            return new Response
            {
                IsSuccess = true,
            };
        }

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                string url = $"{prefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();
                //
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer
                    };
                }
                List<T> list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
