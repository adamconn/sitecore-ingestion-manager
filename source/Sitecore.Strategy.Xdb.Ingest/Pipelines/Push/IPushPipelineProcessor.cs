using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Strategy.Xdb.Ingest.Pipelines.Push
{
    public interface IPushPipelineProcessor
    {
        void Process(PushPipelineArgs args);
    }
}
