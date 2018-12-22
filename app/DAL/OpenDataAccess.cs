using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using Model;

namespace DAL
{
    public class OpenDataAccess
    {
        private HttpClient Client { 
            get
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://data.bep.be/api/records/1.0/search/")
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }
        }

        public IEnumerable<Record> getAllCompanies() // tweaked into companies of a fixed zoning
        {
            string response = Client
                .GetStringAsync("?dataset=societes-de-nos-parcs-dactivite&rows=100&refine.nomparc=Parc+d'activit%C3%A9+%C3%A9conomique+de+Ciney+-+Biron+-+Lienne")
                .Result;
            return JValue.Parse(response).SelectToken("records").ToObject<IEnumerable<Record>>();
        }
    }
}