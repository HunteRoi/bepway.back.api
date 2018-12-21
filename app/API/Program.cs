using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace API {
    public class Program {
        public static void Main (string[] args) {
            RunAsync ();
            //CreateWebHostBuilder(args).Build().Run();
        }

        static HttpClient Client = new HttpClient ();
        static async Task RunAsync () {
            Client.DefaultRequestHeaders.Accept.Clear ();
            Client.DefaultRequestHeaders.Accept.Add (
                new MediaTypeWithQualityHeaderValue ("application/json")
            );
            await GetTaskAsync (new Uri("http://data.bep.be/api/records/1.0search/?dataset=societes-de-nos-parcs-dactivite&rows=100&refine.nomparc=Parc+d%27activité+économique+de+Ciney+-+Biron+-+Lienne"));
        }
        static async Task GetTaskAsync (Uri uri) {
            try {
                var response = await Client.GetStringAsync(uri);
                Console.WriteLine (response);
                /*if (response.IsSuccessStatusCode) {
                    var records = await response.Content.ReadAsAsync<Model.Dataset> ();
                    Console.WriteLine (records.nhits);
                } else {
                    Console.WriteLine ($"berk nope --> {response.StatusCode}");
                }*/
            } catch (Exception e) {
                Console.WriteLine (e.Message);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ();

    }
}