using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace Sitecore.Strategy.Xdb.Ingest.Items
{
    public static class IngestionItemIDs
    {
        public static readonly ID DataHydratorsFolder = new ID("{05E75162-5532-4130-918F-79FC09BBE352}");
        public static readonly ID PushDataHandlersFolder = new ID("{8C55D0AA-D721-489C-9676-64AC96EF19E9}");
        public static readonly ID PullDataHandlersFolder = new ID("{BD7A24C1-AD57-437D-9123-09C56F02980C}");
        public static readonly ID DataReadersFolder = new ID("{D0DE715F-55E4-4691-9E57-3A74DB4F4C7F}");
    }
}