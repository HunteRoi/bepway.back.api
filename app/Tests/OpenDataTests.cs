using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DAL;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class OpenDataTests
    {

        [TestMethod]
        public async Task GetWorks()
        {
            var access = new OpenDataAccess();
            
            IEnumerable<Record> records = await access.getAllCompaniesAsync();
            
            Assert.IsTrue(access.LastResponse.IsSuccessStatusCode, $"L'appel a échoué : {access.LastResponse.StatusCode}");            
        }

        [TestMethod]
        public void HowItWorks()
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            TextInfo ti = ci.TextInfo;

            string normalizedCity = ti.ToTitleCase(ti.ToLower("CINEY"));
            
            Assert.IsTrue(normalizedCity.Equals("Ciney"));
        }

        [TestMethod]
        public async Task ReturnsCompanies()
        {
            var access = new OpenDataAccess();

            IEnumerable<Record> records = await access.getAllCompaniesAsync();       
            
            Assert.IsNotNull(records);
        }
    }
}