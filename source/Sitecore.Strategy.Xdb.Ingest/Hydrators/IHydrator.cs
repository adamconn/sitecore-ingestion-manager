using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Strategy.Xdb.Ingest.Hydrators
{
    public interface IHydrator
    {
        object Target { get; }
        void Hydrate(Stream source, bool applyNullValues = false);
        //bool CanHydrateFromSource(Type sourceType);
    }
}
