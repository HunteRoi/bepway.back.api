using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Newtonsoft.Json.Linq;

namespace Tests {
    [TestClass]
    public class OpenDataTests {

        [TestMethod]
        public async Task GetWorks () {
            var access = new OpenDataAccess ();

            IEnumerable<Record> records = await access.getAllCompaniesAsync ();

            Assert.IsTrue (access.LastResponse.IsSuccessStatusCode, $"L'appel a échoué : {access.LastResponse.StatusCode}");
        }

        [TestMethod]
        public async Task ReturnsCompanies () {
            var access = new OpenDataAccess ();

            IEnumerable<Record> records = await access.getAllCompaniesAsync ();

            Assert.IsNotNull (records);
        }
    }
}