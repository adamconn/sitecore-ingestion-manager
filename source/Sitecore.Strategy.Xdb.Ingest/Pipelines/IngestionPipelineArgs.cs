using Sitecore.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Analytics.Tracking;
using Sitecore.Pipelines;
using Sitecore.Strategy.Xdb.Ingest.DataHandlers;

namespace Sitecore.Strategy.Xdb.Ingest.Pipelines
{
    public class IngestionPipelineArgs : PipelineArgs
    {
        public IngestionPipelineArgs()
        {
            this.DataHandlers = new List<DataHandler>();
        }
        public Contact Contact { get; set; }
        public HttpContext HttpContext { get; set; }
        public List<DataHandler> DataHandlers { get; private set; }
    }
}