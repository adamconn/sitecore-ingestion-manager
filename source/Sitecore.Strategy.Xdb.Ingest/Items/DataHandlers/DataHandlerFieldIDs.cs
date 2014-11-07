using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace Sitecore.Strategy.Xdb.Ingest.Items.DataHandlers
{
    public static class DataHandlerFieldIDs
    {
        public static readonly ID DataReader = new ID("{B5A852A2-35CD-462A-BC32-1527988CF96C}");
        public static readonly ID DataHydrator = new ID("{2DCE1276-D4C3-4635-BE62-EA59BF68A124}");
        public static readonly ID ApplyNullValues = new ID("{71C37955-277D-4EED-BF98-C674FC3F93C6}");
        public static readonly ID Disabled = new ID("{00617E56-FE68-4130-8D9E-830BCD693840}");
    }
}