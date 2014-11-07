using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Sitecore.Strategy.Xdb.Ingest.DataReaders
{
    public class QueryStringDataReader : IDataReader
    {
        public NameValueCollection QueryString { get; set; }

        public QueryStringDataReader(HttpContext context)
        {
            if (context != null)
            {
                this.QueryString = context.Request.QueryString;
            }
        }

        public QueryStringDataReader(string queryString)
        {
            if (!string.IsNullOrEmpty(queryString))
            {
                this.QueryString = HttpUtility.ParseQueryString(queryString);
            }
        }

        public Stream GetDataStream()
        {
            if (this.QueryString == null)
            {
                return null;
            }
            var builder = new StringBuilder();
            var list = new List<string>();
            foreach (string key in this.QueryString.Keys)
            {
                list.Add(string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(this.QueryString[key])));
            }
            var queryString = string.Join("&", list.ToArray());
            return new MemoryStream(Encoding.UTF8.GetBytes(queryString));
        }
    }
}