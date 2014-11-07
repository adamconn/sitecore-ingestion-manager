using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Strategy.Xdb.Ingest.Pipelines.Pull
{
    public interface IPullPipelineProcessor
    {
        void Process(PullPipelineArgs args);
    }
}
