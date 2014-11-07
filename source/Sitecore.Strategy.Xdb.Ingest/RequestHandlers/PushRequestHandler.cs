using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Pipelines;
using Sitecore.Strategy.Xdb.Ingest.Pipelines.Push;

namespace Sitecore.Strategy.Xdb.Ingest.RequestHandlers
{
    public class PushRequestHandler : BasePipelineHandler
    {
        protected override void RunPipeline(HttpContext context)
        {
            var args = new PushPipelineArgs() { HttpContext = context};
            CorePipeline.Run("xdb.ingest.push", args);
        }
    }
}