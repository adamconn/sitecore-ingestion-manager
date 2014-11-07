using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace Sitecore.Strategy.Xdb.Ingest.Items.DataReaders
{
    public static class JsonDataReaderFieldIDs
    {
        public static readonly ID Endpoint = new ID("{9FDB4D9E-0028-484A-B643-C38151C7A764}");
        public static readonly ID UseOAuth = new ID("{026F1C73-3C9B-4BCB-9B27-C766BB39FC8C}");
        public static readonly ID OAuthApiKey = new ID("{9219E94A-1E2C-4EE5-B86C-3C7C0D0F2C17}");
        public static readonly ID OAuthSecret = new ID("{C99A27B4-901B-4BDE-93FF-EC51BFB47257}");
    }
}