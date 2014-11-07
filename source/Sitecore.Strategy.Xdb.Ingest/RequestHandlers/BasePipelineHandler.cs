using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Sitecore.Strategy.Xdb.Ingest.RequestHandlers
{
    public abstract class BasePipelineHandler : IHttpHandler, IRequiresSessionState
    {
        protected BasePipelineHandler()
        {
            this.IsReusable = true;
        }

        public bool IsReusable { get; protected set; }

        protected abstract void RunPipeline(HttpContext context);

        public virtual void ProcessRequest(HttpContext context)
        {
            this.RunPipeline(context);
        }
    }
}