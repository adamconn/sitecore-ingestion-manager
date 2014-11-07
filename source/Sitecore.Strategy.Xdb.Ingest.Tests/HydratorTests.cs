using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Strategy.Xdb.Ingest.Bizo.Hydrators;
using Sitecore.Strategy.Xdb.Ingest.Bizo.Models;
using Sitecore.Strategy.Xdb.Ingest.Samples.Hydrators;

namespace Sitecore.Strategy.Xdb.Ingest.Tests
{
    [TestClass]
    public class HydratorTests
    {
        public TestContext TestContext { get; set; } //this needs to be named TestContext

        [TestMethod, TestCategory("Hydrator tests")]
        public void BizoRedirectHydrator_Test()
        {
            var queryString =
                "company_size=medium_large&functional_area=consultant&functional_area=finance&functional_area=finance_accounting&group=business_professional&group=financial_professional&group=high_net_worth&industry=accounting_and_accounting_services&industry=agriculture&industry=business_services&location=oregon&seniority=executive";
            var model = new BizoData();
            Assert.IsNull(model.CompanySize);
            var hydrator = new BizoRedirectHydrator(model);
            hydrator.Hydrate(new MemoryStream(Encoding.UTF8.GetBytes(queryString)));
            Assert.AreEqual("medium_large", model.CompanySize);
        }

        [TestMethod, TestCategory("Hydrator tests")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"Data\FileTestsPersonalInfo.xml", "info", DataAccessMethod.Sequential)]
        public void ContactPersonalInfoHydrator_File_Test()
        {
            var collection = new NameValueCollection();
            var fields = new string[] {"dob", "first", "gender", "job", "middle", "nickname", "suffix", "last", "title"};
            foreach (var field in fields)
            {
                collection[field] = (string)this.TestContext.DataRow[field];
            }
            var list = new List<string>();
            foreach (string key in collection.Keys)
            {
                list.Add(string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(collection[key])));
            }
            var queryString = string.Join("&", list.ToArray());
            var path = Path.GetTempFileName();
            File.WriteAllText(path, queryString);

            var mockPersonalInfo = new Mock<IContactPersonalInfo>();
            mockPersonalInfo.SetupAllProperties();
            Assert.IsNull(mockPersonalInfo.Object.BirthDate);

            var hydrator = new ContactPersonalInfoHydrator(mockPersonalInfo.Object);
            hydrator.Hydrate(File.OpenRead(path));
            ConfirmContactPersonalInfo(collection, mockPersonalInfo.Object);
        }

        [TestMethod, TestCategory("Hydrator tests")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"Data\QueryStringTestsPersonalInfo.xml", "querystring", DataAccessMethod.Sequential)]
        public void ContactPersonalInfoHydrator_QueryString_Test()
        {
            var mockPersonalInfo = new Mock<IContactPersonalInfo>();
            mockPersonalInfo.SetupAllProperties();
            Assert.IsNull(mockPersonalInfo.Object.BirthDate);

            var queryString = (string)this.TestContext.DataRow["data"];
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", queryString), new HttpResponse(new StringWriter()));

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(queryString));
            var hydrator = new ContactPersonalInfoHydrator(mockPersonalInfo.Object);
            hydrator.Hydrate(stream);
            ConfirmContactPersonalInfo(HttpUtility.ParseQueryString(queryString), mockPersonalInfo.Object);
        }

        protected virtual void ConfirmContactPersonalInfo(NameValueCollection collection, IContactPersonalInfo info)
        {
            //
            //confirm dob
            DateTime? dob = null;
            DateTime tmpDate;
            var success = DateTime.TryParse(collection["dob"], out tmpDate);
            if (success)
            {
                dob = tmpDate;
            }
            Assert.AreEqual(dob, info.BirthDate);
            //
            //
            ConfirmStringProperty(collection["first"], info.FirstName);
            ConfirmStringProperty(collection["gender"], info.Gender);
            ConfirmStringProperty(collection["job"], info.JobTitle);
            ConfirmStringProperty(collection["middle"], info.MiddleName);
            ConfirmStringProperty(collection["nickname"], info.Nickname);
            ConfirmStringProperty(collection["suffix"], info.Suffix);
            ConfirmStringProperty(collection["last"], info.Surname);
            ConfirmStringProperty(collection["title"], info.Title);
        }

        protected virtual void ConfirmStringProperty(string expectedValue, string value)
        {
            var cityValue = expectedValue;
            if (string.IsNullOrWhiteSpace(cityValue))
            {
                cityValue = null;
            }
            else
            {
                if (cityValue.Split(',').All(v => string.IsNullOrWhiteSpace(v)))
                {
                    cityValue = null;
                }
            }
            Assert.AreEqual(cityValue, value);
        }
    }
}
