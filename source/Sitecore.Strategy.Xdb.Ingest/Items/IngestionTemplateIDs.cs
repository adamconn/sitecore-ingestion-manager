using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace Sitecore.Strategy.Xdb.Ingest.Items
{
    public static class IngestionTemplateIDs
    {
        public static readonly ID IngestionDataHandler = new ID("{BD988F63-33C1-48EE-AB20-530097B750EF}");
        public static readonly ID PushDataHandler = new ID("{2C7DF4D0-7562-4142-B186-478F14FB1083}");
        public static readonly ID PullDataHandler = new ID("{392A0004-6D2D-4400-8A60-6040D4FD87EA}");
        //
        //
        public static readonly ID ContactFacetHydrator = new ID("{5AAE3016-3365-4FC6-8749-D199926A8559}");
        //
        //
        public static readonly ID DataReader = new ID("{4FDDC19E-C79B-45E5-BFB4-706BF5FA00BC}");
        public static readonly ID FileSystemDataReader = new ID("{2E9015D5-8796-410E-BD68-DA00EBE7BF84}");
        public static readonly ID QueryStringDataReader = new ID("{9F70979C-7951-4F99-A213-80D0FDFEC9A3}");
        public static readonly ID JsonDataReader = new ID("{B1E3568B-B0A4-4154-BE1F-B32F20E6B6A7}");
    }
}