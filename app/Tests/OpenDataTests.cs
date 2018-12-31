using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Newtonsoft.Json.Linq;

namespace Tests {
    [TestClass]
    public class OpenDataTests {
        private BepwayContext _context;

        [TestMethod]
        public async Task OpenDataWorksLikeACharm () {
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

        private BepwayContext GetContext() {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            DbContextOptions/*<BepwayContext>*/ options = /*(DbContextOptions<BepwayContext>)*/ builder.UseSqlServer(@"Data Source=tcp:bepway.database.windows.net,1433;Initial Catalog=Bepway;User Id=bepwayRoot@bepway.database.windows.net;Password=Pads0u6x;").Options;

            _context = new BepwayContext(options);
            
            return _context;
        }

        [TestMethod]
        public async Task ShouldThrowDbConcurrency() {
        // Arrange
            BepwayContext context1 = GetContext();
            BepwayContext context2 = GetContext();
            BepwayContext testContext;
            Company company1 = await context1.Company.OrderBy(c => c.Id).FirstAsync();
            Company company2 = await context2.Company.OrderBy(c => c.Id).FirstAsync();
            Company customerAfterUpdate;
        // Act
            company2.ImageUrl = "https://coucou.com";
            await context2.SaveChangesAsync();
            company1.ImageUrl = "https://coucou.be";
        // Assert
            Assert.ThrowsException<DbUpdateConcurrencyException>(() => context1.SaveChanges());
            testContext = GetContext();
            customerAfterUpdate = await testContext.Company.FindAsync(company1.Id);
            Assert.AreEqual("https://coucou.com", customerAfterUpdate.ImageUrl);
        }
    }
}
