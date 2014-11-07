using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using Sitecore.Strategy.Xdb.Ingest.Pipelines.Pull;

namespace Sitecore.Strategy.Xdb.Ingest.Pipelines.StartAnalytics
{
    public class StartIngestUsingPullPipeline
    {
        public virtual void Process(PipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (! Sitecore.Analytics.Tracker.Current.Session.Settings.IsFirstRequest)
            {
                return;
            }
            var args2 = new PullPipelineArgs() {HttpContext = HttpContext.Current, Contact = Sitecore.Analytics.Tracker.Current.Contact};
            CorePipeline.Run("xdb.ingest.pull", args2);
        }
    }
}