using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Strategy.Xdb.Ingest.DataReaders
{
    /// <summary>
    /// Reads data from an external system
    /// </summary>
    public interface IDataReader
    {
        Stream GetDataStream();
    }
}
