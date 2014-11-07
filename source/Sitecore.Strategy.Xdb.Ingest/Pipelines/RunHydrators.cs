using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Strategy.Xdb.Ingest.Pipelines
{
    public class RunHydrators : IngestionPipelineArgs
    {
        public virtual void Process(IngestionPipelineArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            var handlers = args.DataHandlers;
            if (handlers == null || handlers.Count == 0)
            {
                return;
            }
            foreach (var handler in handlers)
            {
                var reader = handler.DataReader;
                var hydrator = handler.Hydrator;
                if (reader == null || hydrator == null)
                {
                    continue;
                }
                var data = handler.DataReader.GetDataStream();
                if (data == null)
                {
                    continue;
                }
                hydrator.Hydrate(data, handler.ApplyNullValues);
            }
        }
    }
}