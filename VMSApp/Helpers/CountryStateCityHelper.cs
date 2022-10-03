using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
namespace VMSApp.Helpers
{
    public static class CountryStateCityHelper
    {


        public static string getAccessToken()
        {
            string token = "";

            using (WebClient client = new WebClient())
            {
                client.BaseAddress = ConfigurationManager.AppSettings["CSCAuthApi"];
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("user-email", ConfigurationManager.AppSettings["CSCAuthEmail"]);
                client.Headers.Add("api-token", ConfigurationManager.AppSettings["CSCApiToken"]);
                var result = client.DownloadString("getaccesstoken/");
                var kvp = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                if (kvp != null)
                {
                    token = kvp["auth_token"];
                }

            }
           
            return token;
        }

        internal static List<SelectListItem> getCountries(string token)
        {
            List<SelectListItem> countryList = new List<SelectListItem>();
            using (WebClient client = new WebClient())
            {
                client.BaseAddress = ConfigurationManager.AppSettings["CSCAuthApi"];
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Authorization", "Bearer " + token);
                var result = client.DownloadString("countries/");
                var kvp = JsonConvert.DeserializeObject<JArray>(result);
                if (kvp != null)
                {

                    countryList = kvp.AsEnumerable().Select(x => new SelectListItem()
                      {
                          Text = x["country_name"].ToString(),
                          Value = x["country_name"].ToString()
                      }).ToList();
                }
            }
            return countryList;
        }

        internal static List<SelectListItem> getStates(string country, string token)
        {
            List<SelectListItem> countryList = new List<SelectListItem>();
            using (WebClient client = new WebClient())
            {
                client.BaseAddress = ConfigurationManager.AppSettings["CSCAuthApi"];
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Authorization", "Bearer " + token);
                var result = client.DownloadString("states/" + country);
                var kvp = JsonConvert.DeserializeObject<JArray>(result);
                if (kvp != null)
                {

                    countryList = kvp.AsEnumerable().Select(x => new SelectListItem()
                      {
                          Text = x["state_name"].ToString(),
                          Value = x["state_name"].ToString()
                      }).ToList();
                }
            }
            return countryList;
        }



        internal static List<SelectListItem> getcities(string state, string token)
        {
            List<SelectListItem> cities = new List<SelectListItem>();
            using (WebClient client = new WebClient())
            {
                client.BaseAddress = ConfigurationManager.AppSettings["CSCAuthApi"];
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Authorization", "Bearer " + token);
                var result = client.DownloadString("cities/" + state);
                var kvp = JsonConvert.DeserializeObject<JArray>(result);
                if (kvp != null)
                {

                    cities = kvp.AsEnumerable().Select(x => new SelectListItem()
                    {
                        Text = x["city_name"].ToString(),
                        Value = x["city_name"].ToString()
                    }).ToList();
                }
            }
            return cities;
        }
    }
}
