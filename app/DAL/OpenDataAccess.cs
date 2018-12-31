using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DAL {
    public class OpenDataAccess {
        private HttpClient Client {
            get {
                var client = new HttpClient {
                    BaseAddress = new Uri ("https://data.bep.be/api/records/1.0/search/")
                };
                client.DefaultRequestHeaders.Accept.Clear ();
                client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
                return client;
            }
        }

        public HttpResponseMessage LastResponse { get; private set; }

        public async Task<IEnumerable<Record>> getAllCompaniesAsync () {
            IEnumerable<Record> records = null;

            //LastResponse = await Client.GetAsync("?dataset=societes-de-nos-parcs-dactivite&rows=1400");
            LastResponse = await Client.GetAsync ("?dataset=societes-de-nos-parcs-dactivite&rows=100&refine.nomparc=Parc+d%27activité+économique+de+Ciney+-+Biron+-+Lienne");
            if (LastResponse.IsSuccessStatusCode) {
                string content = await LastResponse.Content.ReadAsStringAsync ();
                records = JValue.Parse (content)
                    .SelectToken ("records")
                    .ToObject<IEnumerable<Record>> ()
                    //.Distinct(new StrictKeyEqualityComparer<Record, string>(r => r.Fields.NomEntreprise))
                    .OrderBy (r => r.Record_timestamp);
            }

            return records;
        }
    }
}