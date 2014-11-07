using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Strategy.Xdb.Ingest.DataReaders;
using Sitecore.Strategy.Xdb.Ingest.Hydrators;

namespace Sitecore.Strategy.Xdb.Ingest.DataHandlers
{
    public class DataHandler
    {
        public IDataReader DataReader { get; set; }
        public IHydrator Hydrator { get; set; }
        public bool ApplyNullValues { get; set; }
    }
}
