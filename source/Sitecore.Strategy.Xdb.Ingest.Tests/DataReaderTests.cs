using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sitecore.Strategy.Xdb.Ingest.DataReaders;
using Newtonsoft.Json;

namespace Sitecore.Strategy.Xdb.Ingest.Tests
{
    [TestClass]
    public class DataReaderTests
    {
        private const string BIZO_API_KEY = "";
        private const string BIZO_OAUTH_API_KEY = "";
        private const string BIZO_OAUTH_SECRET = "";
        private const string DEMANDBASE_API_KEY = "";

        public TestContext TestContext { get; set; } //this needs to be named TestContext

        [TestMethod, TestCategory("DataReader tests")]
        public void JsonDataReader_Demandbase_IP_Test()
        {
            var reader = new JsonDataReader() { Endpoint = "http://api.demandbase.com/api/v2/ip.json" };
            reader.Parameters.Add("key", DEMANDBASE_API_KEY);
            reader.Parameters.Add("query", "24.22.14.68");
            var stream = reader.GetDataStream();
            Assert.IsNotNull(stream);
            var stream2 = new StreamReader(stream, Encoding.UTF8);
            var data = stream2.ReadToEnd();
            Assert.IsNotNull(data);
            var obj = JsonConvert.DeserializeObject(data);
            Assert.IsNotNull(obj);
        }
        [TestMethod, TestCategory("DataReader tests")]
        public void JsonDataReader_Demandbase_IP_Localhost_Test()
        {
            var reader = new JsonDataReader() { Endpoint = "http://api.demandbase.com/api/v2/ip.json" };
            reader.Parameters.Add("key", DEMANDBASE_API_KEY);
            reader.Parameters.Add("query", "127.0.0.1");
            var stream = reader.GetDataStream();
            Assert.IsNull(stream);
        }
        [TestMethod, TestCategory("DataReader tests")]
        public void JsonDataReader_Bizo_Classify_Test()
        {
            var reader = new JsonDataReader() { Endpoint = "http://api.bizographics.com/v4/classify.json" };
            reader.UseOauth = true;
            reader.OAuthApiKey = BIZO_OAUTH_API_KEY;
            reader.OAuthSecret = BIZO_OAUTH_SECRET;
            reader.Parameters.Add("api_key", reader.OAuthApiKey);
            reader.Parameters.Add("secret", reader.OAuthSecret);
            reader.Parameters.Add("ip_address", "206.108.40.109");
            var stream = reader.GetDataStream();
            Assert.IsNotNull(stream);
            var stream2 = new StreamReader(stream, Encoding.UTF8);
            var data = stream2.ReadToEnd();
            Assert.IsNotNull(data);
            var obj = JsonConvert.DeserializeObject(data);
            Assert.IsNotNull(obj);
        }

        [TestMethod, TestCategory("DataReader tests")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"Data\FileTestsPersonalInfo.xml", "info", DataAccessMethod.Sequential)]
        public void QueryStringDataReader_Url_Test()
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
            var path = Path.GetTempFileName();
            var queryString = string.Join("&", list.ToArray());

            var reader = new QueryStringDataReader(queryString);
            var stream = reader.GetDataStream();
            Assert.IsNotNull(stream);
            using (var stream2 = new StreamReader(stream, Encoding.UTF8))
            {
                var data = stream2.ReadToEnd();
                Assert.AreEqual(queryString, data);
            }
        }
    }
}
