using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Sitecore.Collections;
using Sitecore.Diagnostics;
using Sitecore.Extensions.StringExtensions;

namespace Sitecore.Strategy.Xdb.Ingest.DataReaders
{
    public class JsonDataReader : IDataReader
    {
        public JsonDataReader()
        {
            this.Parameters = new HashDictionary<string, string>();
        }
        public string Endpoint { get; set; }
        public bool UseOauth { get; set; }
        public string OAuthApiKey { get; set; }
        public string OAuthSecret { get; set; }
        public IDictionary<string, string> Parameters { get; private set; }

        public virtual Stream GetDataStream()
        {
            Uri uri = null;
            Log.Debug(string.Format("OAuth {0}enabled for this data reader", (this.UseOauth ? string.Empty : "not ")), this);
            if (this.UseOauth)
            {
                uri = GetSignedUri(this.Endpoint, this.OAuthApiKey, this.OAuthSecret, this.Parameters);
            }
            else
            {
                uri = GetUri(this.Endpoint, this.Parameters);
            }
            try
            {
                var request = WebRequest.Create(uri);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                return stream;
            }
            catch (WebException ex)
            {
                Log.Warn(string.Format("This exception may not indicate an error. Check the documentation for the endpoint {0}", this.Endpoint), ex, this);
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                    return null;
                }
            }
            return null;
        }

        
        /// <summary>
        /// Converts a string dictionary into a query string.
        /// </summary>
        /// <param name="parameters">The keys and values that are converted into a format compatible with query strings</param>
        /// <param name="doNotEncode">If false the keys and values will be URL encoded, if true the keys and values will not be URL encoded</param>
        /// <returns>A string that can be used as the query string on a uri</returns>
        protected virtual string GetQueryString(IDictionary<string, string> parameters, bool doNotEncode = false)
        {
            if (parameters == null)
            {
                return null;
            }
            string queryString = null;
            if (doNotEncode)
            {
                queryString = parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)).Aggregate((a, b) => a + "&" + b);
            }
            else
            {
                queryString = parameters.Select(p => string.Format("{0}={1}", HttpUtility.UrlEncode(p.Key), HttpUtility.UrlEncode(p.Value))).Aggregate((a, b) => a + "&" + b);
            }
            return queryString;
        }

        /// <summary>
        /// Gets a Uri with the specified parameters appended to the query string
        /// </summary>
        /// <param name="url">The URL, such as http://localhost/path</param>
        /// <param name="parameters">The keys and values that are appended to the query string</param>
        /// <param name="doNotEncode">If false the keys and values will be URL encoded, if true the keys and values will not be URL encoded</param>
        /// <returns>A Uri with the specified parameters appended to the query string</returns>
        protected virtual Uri GetUri(string url, IDictionary<string, string> parameters, bool doNotEncode = false)
        {
            Assert.ArgumentNotNullOrEmpty(url, "url");
            Assert.ArgumentNotNull(parameters, "parameters");
            var queryString = GetQueryString(parameters, doNotEncode);
            var builder = new UriBuilder(url);
            if (!string.IsNullOrEmpty(builder.Query))
            {
                queryString = builder.Query.Right(builder.Query.Length - 1) + "&" + queryString;
            }
            builder.Query = queryString;
            return builder.Uri;
        }

        /// <summary>
        /// Gets a signed Uri for the specified endpoint with the specified parameters appended to the query string.
        /// This Uri can be used with REST APIs that use OAuth.
        /// </summary>
        /// <param name="url">The url that will have the parameters appended to its query string</param>
        /// <param name="key">OAuth key required by the external system</param>
        /// <param name="secret">OAuth secret required by the external system</param>
        /// <param name="parameters">The keys and values that are appended to the query string</param>
        /// <returns>A signed Uri for the endpoint with the parameters appended to the query string</returns>
        protected virtual Uri GetSignedUri(string url, string key, string secret, IDictionary<string, string> parameters)
        {
            var uri = GetUri(url, parameters);
            string nurl, nreq;
            var oAuth = new OAuthHelper();

            var nounce = oAuth.GenerateNonce();
            var timestamp = oAuth.GenerateTimeStamp();

            var signatureUrl = oAuth.GenerateSignature(
                url: uri,
                consumerKey: key,
                consumerSecret: secret,
                token: string.Empty,
                tokenSecret: string.Empty,
                httpMethod: "GET",
                timeStamp: timestamp,
                nonce: nounce,
                signatureType: OAuthHelper.SignatureTypes.HMACSHA1,
                normalizedUrl: out nurl,
                normalizedRequestParameters: out nreq);

            signatureUrl = HttpUtility.UrlEncode(signatureUrl);

            var parameters2 = new Dictionary<string, string>();
            parameters2.Add("oauth_consumer_key", key);
            parameters2.Add("oauth_nonce", nounce);
            parameters2.Add("oauth_timestamp", timestamp);
            parameters2.Add("oauth_signature_method", "HMAC-SHA1");
            parameters2.Add("oauth_version", "1.0");
            parameters2.Add("oauth_signature", signatureUrl);
            var uri2 = GetUri(uri.ToString(), parameters2, true);
            return uri2;
        }
    }
}