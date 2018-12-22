using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
namespace Bep.Tests
{
    [TestClass]
    public class OpenDataTests
    {
        [TestMethod]
        public async Task GetWorks()
        {
             var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var uri=new Uri("https://data.bep.be/api/records/1.0/search/?dataset=societes-de-nos-parcs-dactivite&rows=100&refine.nomparc=Parc+d%27activité+économique+de+Ciney+-+Biron+-+Lienne");
            
            var response = await client.GetAsync(uri);
            // Console.WriteLine(response);
            Assert.IsTrue(response.IsSuccessStatusCode, $"L'appel a échoué: {response.StatusCode}");
            //Assert.IsNotNull(response);
        }
    }
}
